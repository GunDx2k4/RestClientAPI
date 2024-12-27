using Newtonsoft.Json;
using RestAPIClient.Authentication;
using RestAPIClient.Extensions;
using RestAPIClient.Parameters;

namespace RestAPIClient;

public class RestClient
{
    private readonly HttpClient _httpClient = new();

    private readonly IAuthenticator _authenticator;
    
    public IAuthenticator Authenticator => _authenticator;
    
    public RestClient(Uri uri, IAuthenticator authenticator)
    {
        _httpClient.BaseAddress = uri;
        _authenticator = authenticator;
        _authenticator.DefaultAuthentication(_httpClient);
    }
    
    public async Task<T?> Post<T>(string endPoint, Parameter? parameters = null) where T : class
    { 
        var request = new HttpRequestMessage(HttpMethod.Post, _httpClient.BuildRelativeUri(endPoint)).BuildRequestBodyJson(parameters);
        await Authenticator.RequestAuthentication(request);

        var response = await _httpClient.SendAsync(request);
        var responseData = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<T>(responseData);
        }
        else
        {
            try
            { 
                return JsonConvert.DeserializeObject<T>(responseData);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public async Task<T?> Get<T>(string endPoint, Parameter? query = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _httpClient.BuildRelativeUri(endPoint, query));
        await Authenticator.RequestAuthentication(request);

        var response = await _httpClient.SendAsync(request);
        var responseData = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<T>(responseData);
        }
        else
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(responseData);
            }
            catch (Exception)
            {
                return null;
            }
        }
    } 
}