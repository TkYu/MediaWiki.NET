using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaWikiNET.Models
{
    /// <summary>
    /// see at https://en.wikipedia.org/w/api.php
    /// </summary>
    public abstract class Request
    {
        /// <summary>
        /// Output as a dict
        /// </summary>
        public virtual Dictionary<string, string> ToDictionary()
        {
            var json = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
