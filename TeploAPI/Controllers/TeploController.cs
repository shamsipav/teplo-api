using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TeploAPI.Controllers
{
    public abstract class TeploController : Controller
    {
        public Guid GetUserId()
        {
            string? userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId, out Guid parsedGuid))
                return parsedGuid;
            else
                return Guid.Empty;
        }
    }
}
