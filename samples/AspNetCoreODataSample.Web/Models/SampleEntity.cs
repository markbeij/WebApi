using Newtonsoft.Json;

namespace AspNetCoreODataSample.Web.Models
{
    public class SampleEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
