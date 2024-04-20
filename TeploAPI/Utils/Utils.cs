using System.Security.Claims;

namespace TeploAPI.Utils
{
    public static class Utils
    {
        public static string ToStringUtc(DateTime time)
        {
            return $"DateTime({time.Ticks}, DateTimeKind.Utc)";
        }
        
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            string? userId =  user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId, out Guid parsedGuid))
                return parsedGuid;
            else
                return Guid.Empty;
        }
    }
}
