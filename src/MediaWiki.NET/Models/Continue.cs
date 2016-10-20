using Newtonsoft.Json;

namespace MediaWikiNET.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Continue
    {
        /// <summary>
        /// 
        /// </summary>
        public int sroffset { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("continue")]
        public string continues { get; set; }
    }
}
