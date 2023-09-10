using CachingExample.Models;
using Microsoft.Net.Http.Headers;

namespace CachingExample.Repositories
{
    public class UserService : IUserService
    {
        const string url = "https://api.github.com/users/sedatbozdogan";
        const string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:71.0) Gecko/20100101 Firefox/71.0";
        private readonly IHttpClientFactory _httpClientFactory;
        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<User> GetUser()
        {
            var httpClient = _httpClientFactory.CreateClient(); 
            httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, userAgent);

            var response = await httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);
            return user;
        }

    }
}
