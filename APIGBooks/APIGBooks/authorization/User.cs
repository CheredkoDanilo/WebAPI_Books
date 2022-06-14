using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using APIGBooks.Constant;
using APIGBooks.Model;

namespace APIGBooks.authorization
{
    public class User
    {
        public static UserCredential credentials;
        private static string _clientId;
        private static string _clientSecret;
        public User()
        {
            _clientId = Constants.clientId;
            _clientSecret = Constants.clientSecret; 
        }

        public async Task<string> AuthorizationAsync()
        {
            try
            {
                string[] scopes = { "https://www.googleapis.com/auth/books" };
                credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = _clientId,
                        ClientSecret = _clientSecret,
                    },
                    scopes, "User", CancellationToken.None).Result;
                return "Авторизацію пройдено успішно";
            }
            catch
            {
                return "Не вдалося авторизуватися";
            }
        }
        public async Task<string> RevokAsync()
        {
            try
            {
                if (User.credentials != null)
                User.credentials.RevokeTokenAsync(CancellationToken.None).Wait();
                return "Відкликання доступу пройшло успішно";
            }
            catch
            {
                return "Не вдалося відкликання доступ до Google Books";
            }
        }
        public async Task<List<string>> GetListShelfAsync(string shelf)
        {
            try
            {
                List<string> result = new List<string>();
                if (credentials != null)
                if (credentials.Token.IsExpired(SystemClock.Default))
                {
                    credentials.RefreshTokenAsync(CancellationToken.None).Wait();
                }
                var service = new BooksService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credentials
                });
                var respons = service.Mylibrary.Bookshelves.Volumes.List(shelf).Execute().Items; 
                if (respons != null)
                foreach (var respon in respons)
                {
                    result.Add(respon.VolumeInfo.Title);
                }
                return result;
            }
            catch 
            {
                List<string> result = new List<string>();
                result.Add("Ви не авторизувалися");
                return result;
            }
        }
        public async Task<string> AddShelfAsync(string shelf, string id)
        {
            try
            {
                if (credentials != null)
                    if (credentials.Token.IsExpired(SystemClock.Default))
                    {
                        credentials.RefreshTokenAsync(CancellationToken.None).Wait();
                    }
                var service = new BooksService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credentials
                });
                var respons = service.Mylibrary.Bookshelves.AddVolume($"{shelf}", $"{id}").Execute();
                return "Книгу добавлено на полицю";
            }
            catch
            {
                return "Книгу не вдалося добавити на полицю";
            }
        }
        public async Task<string> DeleteShelfAsync(string shelf, string id)
        {
            try
            {
                if (credentials != null)
                    if (credentials.Token.IsExpired(SystemClock.Default))
                    {
                        credentials.RefreshTokenAsync(CancellationToken.None).Wait();
                    }
                var service = new BooksService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credentials
                });
                var respons = service.Mylibrary.Bookshelves.RemoveVolume(shelf,id).Execute();
                return "Книгу видалено з полиці";
            }
            catch
            {
                return "Книгу не вдалося видалити з полиці";
            }
        }
    }
}

