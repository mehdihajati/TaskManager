using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public Guid? UserId
    {
        get
        {
            var userIdClaims = _contextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (userIdClaims is null)
                return null;
            return Guid.Parse(userIdClaims);
        }
    }

    public string? Email
    {
        get
        {
            var userEmailClaims = _contextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
            if (userEmailClaims is null)
                return null;
            return userEmailClaims;
        }
    }
}