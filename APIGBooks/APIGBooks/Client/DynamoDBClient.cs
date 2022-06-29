using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using APIGBooks.Exeption;
using APIGBooks.Model;

namespace APIGBooks.Client
{
    public class DynamoDBClient : IDynamoDBClient, IDisposable
    {
        private readonly IAmazonDynamoDB _dynamoDB;
        private string _tablename = "GBooks_v1";

        public DynamoDBClient(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDB = dynamoDB;
        }
        public async Task<bool> Create(BookDb book)
        {
            var request = new PutItemRequest
            {
                TableName = _tablename,
                Item = new Dictionary<string, AttributeValue>
                {
                    {"Id", new AttributeValue{ S = book.Id} },
                    {"kind", new AttributeValue{ S = book.kind} },
                    {"etag", new AttributeValue{ S = book.etag} },
                    {"title", new AttributeValue{ S = book.title} },
                    {"authors", new AttributeValue{ S = book.authors} },
                    {"printedPageCount", new AttributeValue{ N = $"{book.printedPageCount}"} },
                    {"categories", new AttributeValue{ S = book.categories} },
                    {"contentVersion", new AttributeValue{ S = book.contentVersion} },
                    {"maturityRating", new AttributeValue{ S = book.maturityRating} },
                    {"publishedDate", new AttributeValue{ S = book.publishedDate} },
                    {"language", new AttributeValue{ S = book.language} },
                    {"canonicalVolumeLink", new AttributeValue{ S = book.canonicalVolumeLink} },
                }
            };
            try
            {
                var response = await _dynamoDB.PutItemAsync(request);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            var item = new DeleteItemRequest
            {
                TableName = _tablename,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue{S = id } }
                }
            };

            try
            {
                var response = await _dynamoDB.DeleteItemAsync(item);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BookDb> Get(string id)
        {
            var item = new GetItemRequest
            {
                TableName = _tablename,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue{S = id } }
                }
            };

            var response = await _dynamoDB.GetItemAsync(item);
            if (response.Item == null || !response.IsItemSet)
                return null;
            var result = response.Item.ToClass<BookDb>();
            return result;
        }

        public async Task<List<BookDb>> GetAll()
        {
            var result = new List<BookDb>();
            var request = new ScanRequest
            {
                TableName = _tablename,

            };
            var response = await _dynamoDB.ScanAsync(request);
            if (response.Items == null || response.Items.Count == 0)
                return null;
            foreach(Dictionary<string, AttributeValue> item in response.Items)
            {
                result.Add(item.ToClass<BookDb>());
            }
            return result;
        }

        public async Task<bool> Update(string id)
        {
            var request = new UpdateItemRequest
            {
                TableName = _tablename,
                Key = new Dictionary<string, AttributeValue>()
                {
                    { "Id", new AttributeValue { S = id } }
                }

            };
            try
            {
                var response = await _dynamoDB.UpdateItemAsync(request);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        
        }
        public void Dispose()
        {
            _dynamoDB.Dispose();
        }
    }
}
