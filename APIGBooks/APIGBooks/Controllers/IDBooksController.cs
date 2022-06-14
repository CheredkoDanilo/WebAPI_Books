using Microsoft.AspNetCore.Mvc;
using APIGBooks.Client;
using APIGBooks.Model;

namespace APIGBooks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IDBooksController : Controller
    {
        [HttpGet(Name = "BookId")]
        public Models BookId(string id)
        {
            Clients client = new Clients();
            return client.GetBookIdAsync(id).Result;
        }

        
    }
}
