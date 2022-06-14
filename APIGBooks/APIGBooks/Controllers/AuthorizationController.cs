using Microsoft.AspNetCore.Mvc;
using APIGBooks.authorization;

namespace APIGBooks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : Controller
    {
        [HttpGet(Name = "Authorization")]
        public void Authoriz()
        {
            Authorizations Authorizations = new Authorizations();
            Authorizations.AuthorizationAsync();
        }
    }
}
