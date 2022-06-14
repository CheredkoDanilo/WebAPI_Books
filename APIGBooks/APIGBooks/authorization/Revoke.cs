namespace APIGBooks.authorization
{
    public class Revoke
    {
        public async Task RevokAsync()
        {
            if (Authorizations.credentials != null)
            Authorizations.credentials.RevokeTokenAsync(CancellationToken.None).Wait();
            return;
        }
    }
}
