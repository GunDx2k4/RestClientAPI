using Newtonsoft.Json;
using RestAPIClient;
using RestAPIClient.Authentication;
using RestAPIClient.Parameters;


var json = new Parameter()
    .AddParameter("name", "John")
    .AddParameter("job", "Developer");


var API = new RestClient(new Uri("https://reqres.in"), new CustomAuthenticator());

var data = await API.Post<object>("api/users", json);

Console.WriteLine(JsonConvert.SerializeObject(data));



data = await API.Get<object>("api/users/2");

Console.WriteLine(JsonConvert.SerializeObject(data));


class CustomAuthenticator : IAuthenticator
{
    public Task DefaultAuthentication(HttpClient httpClient)
    {
        return Task.CompletedTask;
    }

    public Task RequestAuthentication(HttpRequestMessage httpRequestMessage)
    {
        return Task.CompletedTask;
    }
}