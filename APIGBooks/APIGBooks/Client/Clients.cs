using APIGBooks.Constant;
using APIGBooks.Model;
using Newtonsoft.Json;

namespace APIGBooks.Client
{
    public class Clients
    {
        private HttpClient _client;
        private HttpClient _client1;
        private static string _address;
        private static string _address1;
        private static string _kay;

        public Clients()
        {
            _address = Constants.address;
            _address1 = Constants.address1;
            _kay = Constants.kay;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
            _client1 = new HttpClient();
            _client1.BaseAddress = new Uri(_address1);

        }

        public async Task<AudioBook> GetBookIdAsync(string id)
        {          
            var respons = await _client.GetAsync($"volumes/{id}?key={_kay}");
            respons.EnsureSuccessStatusCode();
            var content = respons.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Book>(content);
            var respons1 = await _client1.GetAsync($"search?part=snippet&q={result.volumeInfo.title} аудиокнига&key={_kay}");
            respons1.EnsureSuccessStatusCode();
            var content1 = respons1.Content.ReadAsStringAsync().Result;
            var result1 = JsonConvert.DeserializeObject<YouTubeBook>(content1);
            AudioBook audioBook = new AudioBook();
            audioBook.Book = result;
            audioBook.YouTube = result1;
            return audioBook;
        }

        public async Task<Books> GetBookAsync(string name)
        {
            var respons = await _client.GetAsync($"volumes?q={name}&key={_kay}");
            respons.EnsureSuccessStatusCode();
            var content = respons.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Books>(content);
            return result;
        }
        public async Task<Books> GetAuthorBookAsync(string name, string author)
        {
            var respons = await _client.GetAsync($"volumes?q={name}+inauthor:{author}&key={_kay}");
            respons.EnsureSuccessStatusCode();
            var content = respons.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Books>(content);
            return result;
        }

    }
}

