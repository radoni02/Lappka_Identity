using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/identity/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected Guid GetCurrentUserId()
        {
            var stringId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(stringId is null)
            {
                throw new Exception();
            }
            return Guid.Parse(stringId);
        }
    }
}
