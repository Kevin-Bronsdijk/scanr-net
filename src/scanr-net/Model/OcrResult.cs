using Newtonsoft.Json;

namespace ScanR.Model
{
    public class OcrResult
    {
        internal OcrResult()
        {
        }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}