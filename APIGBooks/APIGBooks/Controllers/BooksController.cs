using Microsoft.AspNetCore.Mvc;
using APIGBooks.Client;
using APIGBooks.Model;

namespace APIGBooks.Controllers
{
    [Route("[action]")]
    [ApiController]
    
    public class BooksController : Controller
    {
        [HttpGet(Name = "Book")]
        public Books Book(string name)
        {
            Clients client = new Clients();
            return client.GetBookAsync(name).Result;
        }

        [HttpGet(Name = "AuthorBook")]
        public Books AuthorBook(string name, string author)
        {
            Clients client = new Clients();
            return client.GetAuthorBookAsync(name, author).Result;
        }
        [HttpGet(Name = "BookId")]
        public Models BookId(string id)
        {
            Clients client = new Clients();
            return client.GetBookIdAsync(id).Result;
        }
    }
}

