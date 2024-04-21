using System.Security.Claims;

namespace TeploAPI.Utils
{
    public static class Utils
    {
        public static string ToStringUtc(DateTime time)
        {
            return $"DateTime({time.Ticks}, DateTimeKind.Utc)";
        }
    }
}
