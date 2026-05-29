using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApiAvalonia.Model;

public class AuthOptions
{
    public const string ISSUER = "Avalonia";
    public const string AUDIENCE = "Avalonia";
    private const string KEY = "dnjvbbDSVJVSdmvklnsjvbbsdvbsdnvkdnvjdbsdhbvhdsbvdksnv";

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }

}