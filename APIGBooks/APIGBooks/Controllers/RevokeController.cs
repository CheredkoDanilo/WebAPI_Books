using Microsoft.AspNetCore.Mvc;
using APIGBooks.authorization;

namespace APIGBooks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RevokeController : Controller
    {
        [HttpGet(Name = "Revoke")]
        public void Revoke()
        {
            Revoke revoke = new Revoke();
            revoke.RevokAsync();
        }
    }
}
