using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UP.Models;

namespace UP.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookGenre> BookGenres { get; set; }

    public virtual DbSet<FrozenBid> FrozenBids { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<ReadingList> ReadingLists { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Rltype> Rltypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleBid> RoleBids { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-ABCR8CG;Database=ShutIkorol;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.CoverPath).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_User");
        });

        modelBuilder.Entity<BookGenre>(entity =>
        {
            entity.ToTable("BookGenre");

            entity.HasOne(d => d.Book).WithMany(p => p.BookGenres)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookGenre_Book");

            entity.HasOne(d => d.Genre).WithMany(p => p.BookGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookGenre_Genre");
        });

        modelBuilder.Entity<FrozenBid>(entity =>
        {
            entity.ToTable("FrozenBid");

            entity.Property(e => e.Bid).HasMaxLength(255);

            entity.HasOne(d => d.Book).WithMany(p => p.FrozenBids)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_FrozenBid_Book");

            entity.HasOne(d => d.User).WithMany(p => p.FrozenBids)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_FrozenBid_User");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Genre1)
                .HasMaxLength(50)
                .HasColumnName("Genre");
        });

        modelBuilder.Entity<ReadingList>(entity =>
        {
            entity.HasKey(e => e.Rlid);

            entity.ToTable("ReadingList");

            entity.Property(e => e.Rlid).HasColumnName("RLId");

            entity.HasOne(d => d.Book).WithMany(p => p.ReadingLists)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReadingList_Book");

            entity.HasOne(d => d.ReadingListTypeNavigation).WithMany(p => p.ReadingLists)
                .HasForeignKey(d => d.ReadingListType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReadingList_RLType");

            entity.HasOne(d => d.User).WithMany(p => p.ReadingLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReadingList_User");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.ToTable("Report");

            entity.Property(e => e.Cause).HasMaxLength(255);

            entity.HasOne(d => d.Book).WithMany(p => p.Reports)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_Report_Book");

            entity.HasOne(d => d.Review).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ReviewId)
                .HasConstraintName("FK_Report_Review");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Report_User");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Review");

            entity.HasOne(d => d.Book).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_Book");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_User");
        });

        modelBuilder.Entity<Rltype>(entity =>
        {
            entity.ToTable("RLType");

            entity.Property(e => e.RltypeId).HasColumnName("RLTypeId");
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<RoleBid>(entity =>
        {
            entity.ToTable("RoleBid");

            entity.Property(e => e.Bid).HasMaxLength(255);

            entity.HasOne(d => d.RequestedRole).WithMany(p => p.RoleBids)
                .HasForeignKey(d => d.RequestedRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleBid_Role");

            entity.HasOne(d => d.User).WithMany(p => p.RoleBids)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleBid_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.MidleName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
