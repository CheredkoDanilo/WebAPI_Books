using APIGBooks.Constant;
using APIGBooks.Model;
using Newtonsoft.Json;
using Google.Apis.Books.v1;

namespace APIGBooks.Client
{
    public class Clients
    {
        private HttpClient _client;
        private static string _address;
        private static string _kay;

        public Clients()
        {
            _address = Constant.Constants.address;
            _kay = Constant.Constants.kay;
            _client = new HttpClient();
            
        }

        public async Task<Models> GetBookIdAsync(string id)
        {
            _address += $"/volumes/{id}?key={_kay}";
            _client.BaseAddress = new Uri(_address);
            var respons = await _client.GetAsync($"");
            var content = respons.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Models>(content);
            return result;
        }

        public async Task<Books> GetBookAsync(string name)
        {
            _address += $"/volumes?q={name}&key={_kay}";
            _client.BaseAddress = new Uri(_address);
            var respons = await _client.GetAsync($"");
            var content = respons.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Books>(content);
            return result;
        }
        public async Task<Books> GetAuthorBookAsync(string name, string author)
        {
            _address += $"/volumes?q={name}+inauthor:{author}&key={_kay}";
            _client.BaseAddress = new Uri(_address);
            var respons = await _client.GetAsync($"");
            var content = respons.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Books>(content);
            return result;
        }

    }
}

