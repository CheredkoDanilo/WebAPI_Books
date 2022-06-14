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
    public class Authorizations
    {
        public static UserCredential credentials;
        private static string _clientId;
        private static string _clientSecret;
        public Authorizations()
        {
            _clientId = Constants.clientId;
            _clientSecret = Constants.clientSecret; 
        }

        public async Task AuthorizationAsync()
        {
            string[] scopes = { "https://www.googleapis.com/auth/books" };
            credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = _clientId,
                    ClientSecret = _clientSecret,
                },
                scopes, "User", CancellationToken.None).Result;
        }
        public async Task<List<string>> GetListShelfAsync(string shelfd)
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
                var respons = service.Mylibrary.Bookshelves.Volumes.List(shelfd).Execute().Items; 
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
    }
}

