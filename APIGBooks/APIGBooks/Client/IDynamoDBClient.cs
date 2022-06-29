using APIGBooks.Model;

namespace APIGBooks.Client
{
    public interface IDynamoDBClient
    {
        public Task<List<BookDb>> GetAll();
        public Task<BookDb> Get(string id);
        public Task<bool> Create(BookDb book);
        public Task<bool> Update(string id);
        public Task<bool> Delete(string id);
    }
}
