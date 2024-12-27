using System.Security.Cryptography;
using System.Text;

namespace RestAPIClient.Authentication;

public abstract class CryptoAuthenticator(
    KeyValuePair<string, string> apiKey,
    KeyValuePair<string, string> passphrase,
    string apiSecretKey)
    : IAuthenticator
{
    public virtual Task DefaultAuthentication(HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add(apiKey.Key, apiKey.Value);
        httpClient.DefaultRequestHeaders.Add(passphrase.Key, passphrase.Value);
        return Task.CompletedTask;
    }

    public abstract Task RequestAuthentication(HttpRequestMessage httpRequestMessage);

    public string SignatureHMACSHA256(string prehash)
    {
        var secretKeyBytes = Encoding.UTF8.GetBytes(apiSecretKey);
        var prehashBytes = Encoding.UTF8.GetBytes(prehash);
        var hmacsha256 = new HMACSHA256(secretKeyBytes);
        var hashmessage = hmacsha256.ComputeHash(prehashBytes);
        return Convert.ToBase64String(hashmessage);
    }
}