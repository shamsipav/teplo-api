using Microsoft.AspNetCore.Mvc;

namespace TeploAPI.Controllers
{
    public abstract class TeploController : Controller
    {
        public Guid GetUserId()
        {
            if (Guid.TryParse(User?.Claims?.FirstOrDefault(x => x.Type == "uid")?.Value, out Guid parsedGuid))
                return parsedGuid;
            else
                return Guid.Empty;
        }
    }
}
