
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaWikiNET.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string batchcomplete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("continue")]
        public Continue continues { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Query query { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Error error { get; set; }
    }
}
