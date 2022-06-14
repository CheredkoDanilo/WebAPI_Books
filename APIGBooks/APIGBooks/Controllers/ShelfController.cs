using Microsoft.AspNetCore.Mvc;

using APIGBooks.authorization;

namespace APIGBooks.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ShelfController : Controller
    {
        [HttpGet(Name = "List shelf")]
        public List<string> ListShelf(string shealf)
        {
            User ListShelf = new User();
            return ListShelf.GetListShelfAsync(shealf).Result;
        }

        [HttpGet(Name = "Add book shelf")]
        public string AddBookShelf(string shealf, string id)
        {
            User AddBookShelf = new User();
            return AddBookShelf.AddShelfAsync(shealf, id).Result;
        }
        [HttpGet(Name = "Delete book shelf")]
        public string DeleteBookShelf(string shealf, string id)
        {
            User DeleteBookShelf = new User();
            return DeleteBookShelf.DeleteShelfAsync(shealf, id).Result;
        }
    }
}
