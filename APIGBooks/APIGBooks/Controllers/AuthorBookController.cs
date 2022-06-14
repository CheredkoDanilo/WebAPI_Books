using Microsoft.AspNetCore.Mvc;
using APIGBooks.Client;
using APIGBooks.Model;

namespace APIGBooks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorBookController : Controller
    {

        [HttpGet(Name = "AuthorBook")]
        public Books AuthorBook(string name, string author)
        {
            Clients client = new Clients();
            return client.GetAuthorBookAsync(name, author).Result;
        }
    }
}
