using System;
using System.Collections.Generic;
using System.Text;
using MediaWikiNET.Models;
using Newtonsoft.Json;

namespace MediaWikiNET
{
    /// <summary>
    /// MediaWiki.NET
    /// </summary>
    public partial class MediaWiki
    {
        /// <summary>
        /// Set true if you want to use https
        /// </summary>
        public bool UseHTTPS { get; set; }

        /// <summary>
        /// Current language
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Api path with scheme and language
        /// </summary>
        public string RequestPath
        {
            get { return $"{(UseHTTPS ? "https" : "http")}://{Language}.{Host}{Path}"; }
            set
            {
                var uri = new Uri(value);
                var req = Http.Get(value);
                if (!req.Contains("MediaWiki API"))
                    throw new Exception("url is not valid!");
                UseHTTPS = uri.Scheme == "https";
                Language = uri.Host.Split('.')[0];
                Host = uri.Host.Remove(0, Language.Length + 1);
                Path = uri.AbsolutePath;
            }
        }
        /// <summary>
        /// The wiki site's api path
        /// If your api path is https://en.wikipedia.org/w/api.php
        /// This field will be /w/api.php
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The wiki site's host without language
        /// If your api path is https://en.wikipedia.org/w/api.php
        /// This field will be wikipedia.org
        /// </summary>
        public string Host { get; private set; }
        /// <summary>
        /// Constructor
        /// Please ensure your apiPath can be open in browser.
        /// </summary>
        /// <param name="apiPath">will use wikipedia default api path</param>
        public MediaWiki(string apiPath = null)
        {
#if NETSTANDARD1_3 || NETSTANDARD1_6
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
            if (string.IsNullOrEmpty(apiPath))
            {
                //default:
                //https://en.wikipedia.org/w/api.php
                UseHTTPS = true;
                Language = Languages.English;
                Host = "wikipedia.org";
                Path = "/w/api.php";
            }
            else
            {
                RequestPath = apiPath;
            }
        }

        /// <summary>
        /// Make a search
        /// </summary>
        /// <param name="request">a search request</param>
        /// <returns></returns>
        public SearchResult Search(Request request)
        {
            var result = Http.Get(RequestPath, request.ToDictionary());
            return JsonConvert.DeserializeObject<SearchResult>(result);
        }

        /// <summary>
        /// Make a search
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <returns></returns>
        public SearchResult Search(string keyword)
        {
            var result = Http.Get(RequestPath, new SearchRequest(keyword).ToDictionary());
            return JsonConvert.DeserializeObject<SearchResult>(result);
        }
    }
}
