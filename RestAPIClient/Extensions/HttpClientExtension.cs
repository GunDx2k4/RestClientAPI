using RestAPIClient.Parameters;

namespace RestAPIClient.Extensions;

public static class HttpClientExtension
{
    public static Uri? BuildRelativeUri(this HttpClient httpClient, string relativeUri, Parameter? query = null)
    {
        if (httpClient.BaseAddress == null) return null;
        var uriBuilder = new UriBuilder(httpClient.BaseAddress)
        {
            Path = relativeUri
        };
        if (query != null)
        {
            uriBuilder.Query = ParameterFactory.CreateQueryStringParameter(query);
        }
        return uriBuilder.Uri;
    }
}