using Newtonsoft.Json;

namespace CachingExample.Models
{
    public class User
    {
        [JsonProperty("login")]
        public string Login { get; set; }
        public string From { get; set; }
    }
}
