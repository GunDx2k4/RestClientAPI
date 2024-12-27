namespace RestAPIClient.Authentication;

public interface IAuthenticator
{
    Task DefaultAuthentication(HttpClient httpClient);

    Task RequestAuthentication(HttpRequestMessage httpRequestMessage);
}