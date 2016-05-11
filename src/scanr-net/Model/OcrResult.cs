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

    public class OcrMultiPageResult
    {
        internal OcrMultiPageResult()
        {
        }

        [JsonProperty("text")]
        public string[] Text { get; set; }
    }
}