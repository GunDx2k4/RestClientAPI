using System.Web;
using Newtonsoft.Json;

namespace RestAPIClient.Parameters;

public static class ParameterFactory
{
    public static string CreateJsonParameter(Parameter parameter)
    {
        var Parameters = parameter.Parameters;
        return Parameters.Count < 0 ? string.Empty : JsonConvert.SerializeObject(Parameters);
    }

    public static string CreateQueryStringParameter(Parameter parameter)
    {
        var Parameters = parameter.Parameters;
        var isIEnumerable = Parameters.Values.Any(param => param is IEnumerable<object>);
        if (isIEnumerable) throw new Exception("Has IEnumerable");
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        foreach (var data in Parameters)
        {
            queryString[data.Key] = data.Value.ToString();
        }
        return queryString.ToString() ?? string.Empty;;
    }
}