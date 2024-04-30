using System.Security.Claims;

namespace TeploAPI.Utils.Extentions;

public static class ClaimsPrincipalExtentions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        string? userId =  user.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (Guid.TryParse(userId, out Guid parsedGuid))
            return parsedGuid;

        throw new Exception("Не удалось найти идентификатор пользователя в Claims");
    }
}