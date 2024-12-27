using System.Web;
using Newtonsoft.Json;

namespace RestAPIClient.Parameters;

public static class ParameterFactory
{
    public static string CreateJsonStringParameter(Parameter parameter)
    {
        var Parameters = parameter.Parameters;
        return Parameters.Count < 0 ? string.Empty : JsonConvert.SerializeObject(Parameters);
    }

    public static string CreateQueryStringParameter(Parameter parameter)
    {
        var Parameters = parameter.Parameters;
        Parameters.Values.ToList().ForEach(p =>
        {
            if (p is IEnumerable<object>)
                throw new ArgumentException(
                    $"Expected IEnumerable<object>, but got {p?.GetType().Name ?? "null"}.",
                    nameof(p));
        });
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        foreach (var data in Parameters)
        {
            queryString[data.Key] = data.Value?.ToString();
        }
        return queryString.ToString() ?? string.Empty;
    }
}