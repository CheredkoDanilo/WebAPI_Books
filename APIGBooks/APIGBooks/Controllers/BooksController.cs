using Microsoft.AspNetCore.Mvc;
using APIGBooks.Client;
using APIGBooks.Model;

namespace APIGBooks.Controllers
{
    [Route("[action]")]
    [ApiController]

    public class BooksController : Controller
    {

        private readonly IDynamoDBClient _dynamoDBClient;
        private readonly Clients _client;


        public BooksController(IDynamoDBClient dynamoDBClient, Clients client)
        {
            _dynamoDBClient = dynamoDBClient;
            _client = client;
        }
        [HttpGet(Name = "GetAllDynamoDb")]
        public async Task<List<BookDb>> GetAllDynamoDb()
        {
            var response = await _dynamoDBClient.GetAll();
            if (response == null)
                return null;
            var result = response
                .Select(x => new BookDb()
                {
                    Id = x.Id,
                    kind = x.kind,
                    etag = x.etag,
                    title = x.title,
                    authors = x.authors,
                    printedPageCount = x.printedPageCount,
                    publishedDate = x.publishedDate,
                    maturityRating = x.maturityRating,
                    canonicalVolumeLink = x.canonicalVolumeLink,
                    categories = x.categories,
                    contentVersion = x.contentVersion,
                    language = x.language,
                }).ToList();
            return result;
        }
        [HttpGet(Name = "GetDynamoDb")]
        public async Task<BookDb> GetDynamoDb(string id)
        {
            var result = await _dynamoDBClient.Get(id);
            if (result == null)
                return null;
            var user = new BookDb
            {
                Id = result.Id,
                kind = result.kind,
                etag = result.etag,
                title = result.title,
                authors = result.authors,
                categories = result.categories,
                contentVersion = result.contentVersion,
                canonicalVolumeLink = result.canonicalVolumeLink,
                printedPageCount = result.printedPageCount,
                publishedDate = result.publishedDate,
                language = result.language,
                maturityRating = result.maturityRating
            };
            return user;
        }
        [HttpPost(Name = "PostDynamoDb")]
        public async Task<IActionResult> PostDynamoDb([FromBody] Book book)
        {
            var bookDb = new BookDb
            {
                Id = book.Id,
                kind = book.kind,
                etag = book.etag,
                title = book.volumeInfo.title,
                authors = book.volumeInfo.authors[0],
                categories = book.volumeInfo.categories[0],
                contentVersion = book.volumeInfo.contentVersion,
                canonicalVolumeLink = book.volumeInfo.canonicalVolumeLink,
                printedPageCount = book.volumeInfo.printedPageCount,
                publishedDate = book.volumeInfo.publishedDate,
                language = book.volumeInfo.language,
                maturityRating = book.volumeInfo.maturityRating
            };
            var result = await _dynamoDBClient.Create(bookDb);
            if (result == false)
            {
                return BadRequest("Книгу не вдалося створити");
            }
            return Ok("Книгу створено");
        }
        [HttpPut(Name = "PutDynamoDb")]
        public async Task<IActionResult> PutDynamoDb(string id)
        {
            var result = await _dynamoDBClient.Update(id);
            if (result == false)
            {
                return BadRequest("Книгу не вдалося обновити");
            }
            return Ok("Книгу оновлено");
        }
        [HttpDelete(Name = "DeleteDynamoDb")]
        public async Task<IActionResult> DeleteDynamoDb(string id)
        {
            var result = await _dynamoDBClient.Delete(id);
            if (result == false)
            {
                return BadRequest("Книгу не вдалося видалити");
            }
            return Ok("Книгу видалено");
        }

        [HttpGet(Name = "Book")]
        public Books Book(string name)
        {
            return _client.GetBookAsync(name).Result;
        }

        [HttpGet(Name = "AuthorBook")]
        public Books AuthorBook(string name, string author)
        {
            return _client.GetAuthorBookAsync(name, author).Result;
        }
        [HttpGet(Name = "BookId")]
        public async Task<AudioBook> BookId(string id)
        {
            var book = _client.GetBookIdAsync(id).Result.Book;
            var bookDb = new BookDb
            {
                Id = book.Id,
                kind = book.kind,
                etag = book.etag,
                title = book.volumeInfo.title,
                authors = book.volumeInfo.authors[0],
                categories = book.volumeInfo.categories[0],
                contentVersion = book.volumeInfo.contentVersion,
                canonicalVolumeLink = book.volumeInfo.canonicalVolumeLink,
                printedPageCount = book.volumeInfo.printedPageCount,
                publishedDate = book.volumeInfo.publishedDate,
                language = book.volumeInfo.language,
                maturityRating = book.volumeInfo.maturityRating
            };
            await _dynamoDBClient.Create(bookDb); 
            return _client.GetBookIdAsync(id).Result;
        }
    }
}

