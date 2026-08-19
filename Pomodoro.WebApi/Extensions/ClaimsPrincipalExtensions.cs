using System.Security.Claims;

namespace Pomodoro.WebApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? user.FindFirst("sub")?.Value;

        if (string.IsNullOrWhiteSpace(userIdString))
            throw new UnauthorizedAccessException("User context is missing or unauthenticated");
        return Guid.Parse(userIdString);
    }
}