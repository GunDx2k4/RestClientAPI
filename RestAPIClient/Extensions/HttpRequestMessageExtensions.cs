using System.Text;
using RestAPIClient.Parameters;

namespace RestAPIClient.Extensions;

public static class HttpRequestMessageExtensions
{
    public static HttpRequestMessage BuildRequestBodyJson(this HttpRequestMessage request,Parameter? parameters)
    {
        if (parameters == null) return request;
        var content = new StringContent(ParameterFactory.CreateJsonParameter(parameters), Encoding.UTF8, "application/json");
        request.Content = content;
        return request;
    }
}