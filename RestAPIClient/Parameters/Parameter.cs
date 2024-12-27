using System.Text;

namespace RestAPIClient.Parameters;

public class Parameter
{
    public readonly Dictionary<string, object> Parameters = new();
    
    public Parameter AddParameter(string key, object value)
    {
        Parameters[key] = value;
        return this; 
    }
    
    public Parameter AddParameterList(string key, Parameter values)
    {
        var parameters = new List<Dictionary<string, object>> { (values.Parameters) };
        Parameters[key] = parameters;
        return this;
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        foreach (var (key, value) in Parameters)
        {
            result.Append($"Key: {key}, Value: {value}\n");
        }
        return result.ToString();
    }
}