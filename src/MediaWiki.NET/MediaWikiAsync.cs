#if !NET20 && !NET40
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaWikiNET.Models;
using Newtonsoft.Json;

namespace MediaWikiNET
{
    public partial class MediaWiki
    {

        /// <summary>
        /// Make a search
        /// </summary>
        /// <param name="request">a search request</param>
        /// <returns></returns>
        public async Task<SearchResult> SearchAsync(Request request)
        {
            var result = await Http.GetAsync(RequestPath, request.ToDictionary());
            return JsonConvert.DeserializeObject<SearchResult>(result);
        }

        /// <summary>
        /// Make a search
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <returns></returns>
        public async Task<SearchResult> SearchAsync(string keyword)
        {
            var result = await Http.GetAsync(RequestPath, new SearchRequest(keyword).ToDictionary());
            return JsonConvert.DeserializeObject<SearchResult>(result);
        }
    }
}
#endif