using Microsoft.AspNetCore.Mvc;
using APIGBooks.Client;

namespace APIGBooks.Controllers
{
    [Route("[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly User _user;
        public UserController(User user)
        {
            _user = user;
        }
        [HttpGet(Name = "Authorization")]
        public string Authorizations()
        {
            return _user.AuthorizationAsync().Result;
        }

        [HttpGet(Name = "Revoke")]
        public string Revoke()
        {
            return _user.RevokAsync().Result;
        }
    }
}
