using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace APISpaceSRM.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : Controller
    {
       
        [HttpPost]
        [ActionName("Login")]
        public async Task<ActionResult> Login()
        {
            return Ok();
        }
        [HttpPost]
        [ActionName("Register")]
        public async Task<ActionResult> Register()
        {
            return Ok();
        }
    }
}
