using Microsoft.AspNetCore.Mvc;
using APIGBooks.authorization;

namespace APIGBooks.Controllers
{
    [Route("[action]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet(Name = "Authorization")]
        public string Authorizations()
        {
            User Authorizations = new User();
            return Authorizations.AuthorizationAsync().Result;
        }

        [HttpGet(Name = "Revoke")]
        public string Revoke()
        {
            User Revoke = new User();
            return Revoke.RevokAsync().Result;
        }
    }
}
