using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskManager.API.Extentions;

public static class AuthonticationExtention
{
    public static IServiceCollection AddJwtAuthontication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                        ValidateLifetime = true,
                    };
                });
        return services;
    }
}
