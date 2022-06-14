using Microsoft.AspNetCore.Mvc;
using APIGBooks.Client;
using APIGBooks.Model;

namespace APIGBooks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        [HttpGet(Name = "Book")]
        public Books Book(string name)
        {
            Clients client = new Clients();
            return client.GetBookAsync(name).Result;
        }

    }
}
