using Microsoft.AspNetCore.Mvc;
using APIGBooks.Client;

namespace APIGBooks.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ShelfController : Controller
    {
        private readonly User _user;
        public ShelfController(User user)
        {
            _user = user;
        }
        [HttpGet(Name = "List shelf")]
        public List<string> ListShelf(string shealf)
        {
            return _user.GetListShelfAsync(shealf).Result;
        }

        [HttpPost(Name = "Add book shelf")]
        public string AddBookShelf(string shealf, [FromBody] string id)
        {
            return _user.AddShelfAsync(shealf, id).Result;
        }
        [HttpDelete(Name = "Delete book shelf")]
        public string DeleteBookShelf(string shealf, string id)
        {
            return _user.DeleteShelfAsync(shealf, id).Result;
        }
    }
}
