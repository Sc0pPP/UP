using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SkiaSharp.HarfBuzz;
using UP.Context;
using UP.Models;
using UP.Models.Requests;
using WebApiAvalonia.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddCors();
var app = builder.Build();



app.UseAuthentication();
app.UseAuthorization();
app.UseCors(builder=>builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


//Auth

app.MapPost("/api/auth/login",(AuthData data,MyDbContext context)=>{
    if (context.Users.ToList().FirstOrDefault(u => u.Login == data.Login && u.Password == data.Password) != null)
    {
        var claims = new List<Claim>{new Claim(ClaimTypes.Name, data.Login)};
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(24)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256)
        );
        return Results.Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
    else
    {
        return Results.Unauthorized();
    }
});


    app.MapPost("/api/auth/registration",
        async (Registr reg, MyDbContext context) =>
        {
            if (string.IsNullOrWhiteSpace(reg.Login) ||
                string.IsNullOrWhiteSpace(reg.Password) ||
                string.IsNullOrWhiteSpace(reg.RepeatPassword))
            {
                return Results.BadRequest("Не все поля регистрации заполнены");
            }

            if (reg.Password != reg.RepeatPassword)
            {
                return Results.BadRequest("Пароли не совпадают");
            }

            if (await context.Users.AnyAsync(u => u.Login == reg.Login))
            {
                return Results.BadRequest("Логин занят");
            }

            var user = new User
            {
                Login = reg.Login,
                Password = reg.Password,
                FirstName = reg.FirstName,
                LastName = reg.LastName,
                MidleName = reg.MidleName,
                Email = reg.Email,
                PhoneNumber = reg.PhoneNumber,
                Role = 1,
                IsFrozen = false
            };

           
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return Results.Ok(new
            {
                user.UserId,
                user.Login,
                user.FirstName,
                user.LastName,
                user.MidleName,
                user.Email,
                user.PhoneNumber,
                user.Role,
                user.IsFrozen
            });
        });

//Users
    app.MapGet("/api/users", (int? id,MyDbContext context) =>
    {
        if (id == null)
        {
            return Results.Ok(context.Users.ToList());
        }
        else
        {
            return Results.Ok( context.Users.Where(u => u.UserId == id).ToList());
        }
        
    });


//books

    app.MapGet("/api/books", async (string? name, MyDbContext context) =>
    {
        if (name == null)
        {
            return Results.Ok(await context.Books.ToListAsync());
        }
        else
        {
            return Results.Ok(await context.Books.FirstOrDefaultAsync(u => u.Name == name));
        }
    });

    app.MapPost("/api/books/add", async (AddBook addbook, MyDbContext context) =>
    {
        if (string.IsNullOrWhiteSpace(addbook.Name) ||
            string.IsNullOrWhiteSpace(addbook.Description) ||
            string.IsNullOrWhiteSpace(addbook.Content))
        {
            return Results.BadRequest("Не все поля книги заполнены");
        }
        
        var book = new Book
        {
            Name = addbook.Name,
            Author = addbook.Author,
            Description = addbook.Description,
            IsFrozen = addbook.IsFrozen,
            Content = addbook.Content,
            Genre = addbook.GenreIds.FirstOrDefault(),
            Review = addbook.Review,
            CoverPath = addbook.CoverPath
        };
        
        context.Books.Add(book);
        await context.SaveChangesAsync();

        foreach (var genreId in addbook.GenreIds.Distinct())
        {
            context.BookGenres.Add(new BookGenre
            {
                BookId = book.BookId,
                GenreId = genreId
            });
        }

        await context.SaveChangesAsync();
        
        return Results.Ok(new
        {
            book.BookId,
            book.Name,
            book.Author,
            book.Description,
            book.IsFrozen,
            book.CoverPath,
            book.Content,
            book.Genre,
            book.Review
        });
    });

    app.MapPost("/api/books/addgenres", async (AddGenre addgenre, MyDbContext context) =>
    {
        BookGenre g = new BookGenre
        {
            BookId = addgenre.BookId,
            GenreId = addgenre.GenreId
        };
        
        context.BookGenres.Add(g);
        await context.SaveChangesAsync();
        return Results.Ok(g.BookGenreId);
    });

    app.MapGet("/api/books/genres", async (MyDbContext context) =>
    {
        return Results.Ok(await context.Genres.ToListAsync());
    });

//ReadingList
    app.MapGet("/api/readinglists", async (int userId, int type, MyDbContext context) =>
    {
        var lists = await context.ReadingLists
            .Include(r => r.Book)
            .ThenInclude(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .Include(r => r.Book.AuthorNavigation)
            .Where(r => r.UserId == userId && r.ReadingListType == type)
            .Select(r => new ReadingListResponse
            {
                Rlid = r.Rlid,
                UserId = r.UserId,
                BookId = r.BookId,
                ReadingListType = r.ReadingListType,
                Book = r.Book,
                Genres = r.Book.BookGenres.Select(bg => bg.Genre.Genre1).ToList()
            })
            .ToListAsync();

        return Results.Ok(lists);
    });

    app.MapPost("/api/readinglists/add", async (AddRL addrl,MyDbContext context) =>
    {
        ReadingList rl = new ReadingList()
        {
            UserId = addrl.UserId,
            BookId = addrl.BookId,
            ReadingListType = addrl.ReadingListType
        };
        context.ReadingLists.Add(rl);
        await context.SaveChangesAsync();
        return Results.Ok(rl.Rlid);
    });

    app.MapPatch("/api/readinglists/edit", async (EditRL editrl, MyDbContext context) =>
    {
        var temprl = await context.ReadingLists.FindAsync(editrl.ReadingListId);

        if (temprl == null)
        {
            return Results.NotFound();
        }

        temprl.ReadingListType = editrl.ReadingListType;
        await context.SaveChangesAsync();
        return Results.Ok();
    });

    app.MapDelete("/api/readinglists/{id:int}", async (int id, MyDbContext context) =>
    {
        var readingList = await context.ReadingLists.FindAsync(id);

        if (readingList == null)
        {
            return Results.NotFound();
        }

        context.ReadingLists.Remove(readingList);
        await context.SaveChangesAsync();
        return Results.Ok();
    });

//Reports/Bids
    app.MapPost("/api/reports", (BidRequest bidRequest,MyDbContext context) =>
    {
        if (bidRequest.Type == BidTypes.ReviewReport)
        {
            Report report = new Report()
            {
                UserId = bidRequest.UserId,
                BookId = null,
                ReviewId = bidRequest.ReviewId,
                Cause = bidRequest.Cause,
                IsChecked = false,
                ReportedUserId = null
            };
            context.Reports.Add(report);
        }

        if (bidRequest.Type == BidTypes.BookReport)
        {
            Report report = new Report()
            {
                UserId = bidRequest.UserId,
                BookId = bidRequest.BookId,
                ReviewId = null,
                Cause = bidRequest.Cause,
                IsChecked = false,
                ReportedUserId = null
            };
            context.Reports.Add(report);
        }

        if (bidRequest.Type == BidTypes.AuthorReport)
        {
            Report report = new Report()
            {
                UserId = bidRequest.UserId,
                BookId = null,
                ReviewId = null,
                Cause = bidRequest.Cause,
                IsChecked = false,
                ReportedUserId = bidRequest.ReportedUserId
            };
            context.Reports.Add(report);
        }

        if (bidRequest.Type == BidTypes.FreezeBookReport)
        {
            FrozenBid frozenBid = new FrozenBid()
            {
                UserId = bidRequest.UserId,
                BookId = bidRequest.BookId,
                Bid = bidRequest.Cause
            };
            context.FrozenBids.Add(frozenBid);
        }

        if (bidRequest.Type == BidTypes.FreezeAuthorReport)
        {
            FrozenBid frozenBid = new FrozenBid()
            {
                UserId = bidRequest.UserId,
                ReportedUserId =bidRequest.ReportedUserId,
                Bid = bidRequest.Cause
            };
            context.FrozenBids.Add(frozenBid);
        }

        if (bidRequest.Type == BidTypes.FreezeUserReport)
        {
            FrozenBid frozenBid = new FrozenBid()
            {
                UserId = bidRequest.UserId,
                //ReportedUserId = _appState.CurrentReview!.UserId,
                Bid = bidRequest.Cause
            };
            context.FrozenBids.Add(frozenBid);
        }

        if (bidRequest.Type == BidTypes.AuthorBid)
        {
            RoleBid rb = new RoleBid()
            {
                UserId = bidRequest.UserId,
                Bid = bidRequest.Cause,
                RequestedRoleId = 2,
                IsChecked = false
            };
            context.RoleBids.Add(rb);
        }
        
        context.SaveChanges();
        return Results.Ok();
    });
    

    
app.Run();
