using Microsoft.AspNetCore.Mvc;

using APIGBooks.authorization;

namespace APIGBooks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListShelfController : Controller
    {
        [HttpGet(Name = "List shelf")]
        public List<string> List(string shealf)
        {
            Authorizations Authorizations = new Authorizations();
            return Authorizations.GetListShelfAsync(shealf).Result;
        }
    }
}
