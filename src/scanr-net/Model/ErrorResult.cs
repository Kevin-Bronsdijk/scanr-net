using Newtonsoft.Json;

namespace ScanR.Model
{
    public class ErrorResult
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}