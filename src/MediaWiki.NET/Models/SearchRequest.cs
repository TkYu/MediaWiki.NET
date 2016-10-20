
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaWikiNET.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchRequest : MainModuleRequest
    {
        /// <summary>
        /// A search request
        /// </summary>
        /// <param name="query">keyWords</param>
        public SearchRequest(string query)
        {
            srsearch = query;
        }

        /// <inheritdoc />
        public override string action { get; set; } = "query";

        /// <inheritdoc />
        public override string format { get; set; } = "json";

        /// <inheritdoc />
        public override Dictionary<string, string> ToDictionary()
        {
            var lst = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(action)) lst.Add("action", action);
            if (!string.IsNullOrEmpty(format)) lst.Add("format", format);

            if (!string.IsNullOrEmpty(list)) lst.Add("list", list);
            if (!string.IsNullOrEmpty(srsearch)) lst.Add("srsearch", srsearch);
#if NET20
                if (srinfo!=null)
                {
                    var tmp = new System.Text.StringBuilder();
                    foreach (var i in srinfo)
                        tmp.Append("|" + i);
                    lst.Add("srinfo", tmp.ToString().Remove(0,1).ToLower());
                }
                if (srnamespace!=null)
                {
                    var tmp = new System.Text.StringBuilder();
                    foreach (var i in srnamespace)
                        tmp.Append("|" + i);
                    lst.Add("srnamespace", tmp.ToString().Remove(0, 1).ToLower());
                }
                if (srprop!=null)
                {
                    var tmp = new System.Text.StringBuilder();
                    foreach (var i in srprop)
                        tmp.Append("|" + i);
                    lst.Add("srprop", tmp.ToString().Remove(0, 1).ToLower());
                }
#else
            if (srinfo != null) lst.Add("srinfo", string.Join("|", srinfo).ToLower());
            if (srnamespace != null) lst.Add("srnamespace", string.Join("|", srnamespace).ToLower());
            if (srprop != null) lst.Add("srprop", string.Join("|", srprop).ToLower());
#endif
            if (srlimit != 0) lst.Add("srlimit", srlimit.ToString());
            if (sroffset != 0) lst.Add("sroffset", sroffset.ToString());
            if (srredirects != null) lst.Add("srredirects", srredirects.Value.ToString().ToLower());
            if (srwhat != What.Title) lst.Add("srwhat", srwhat.ToString().ToLower());

            if (maxlag != 0) lst.Add("maxlag", maxlag.ToString());
            if (smaxage != 0) lst.Add("smaxage", smaxage.ToString());
            if (maxage != 0) lst.Add("maxage", maxage.ToString());
            if (!string.IsNullOrEmpty(assert)) lst.Add("assert", assert);
            if (!string.IsNullOrEmpty(assertuser)) lst.Add("assertuser", assertuser);
            if (!string.IsNullOrEmpty(requestid)) lst.Add("requestid", requestid);
            if (servedby != null) lst.Add("servedby", servedby.Value.ToString().ToLower());
            if (curtimestamp != null) lst.Add("curtimestamp", curtimestamp.Value.ToString().ToLower());
            if (!string.IsNullOrEmpty(origin)) lst.Add("origin", origin);
            if (!string.IsNullOrEmpty(uselang)) lst.Add("uselang", uselang);
            if (!string.IsNullOrEmpty(centralauthtoken)) lst.Add("centralauthtoken", centralauthtoken);
            return lst;
        }

        /// <summary>
        /// Which lists to get.
        /// Values(separate with | or alternative): abusefilters, abuselog, allcategories, alldeletedrevisions, allfileusages, allimages, alllinks, allpages, allredirects, allrevisions, alltransclusions, allusers, backlinks, betafeatures, blocks, categorymembers, centralnoticelogs, checkuser, checkuserlog, contenttranslation, contenttranslationcorpora, contenttranslationlangtrend, contenttranslationstats, contenttranslationsuggestions, cxpublishedtranslations, cxtranslatorstats, deletedrevs, embeddedin, exturlusage, filearchive, gadgetcategories, gadgets, geosearch, gettingstartedgetpages, globalallusers, globalblocks, globalgroups, imageusage, iwbacklinks, langbacklinks, logevents, mmsites, mystashedfiles, oldreviewedpages, pagepropnames, pageswithprop, prefixsearch, projectpages, protectedtitles, querypage, random, recentchanges, search, tags, usercontribs, users, watchlist, watchlistraw, wblistentityusage, wikisets
        /// Maximum number of values is 50 (500 for bots).
        /// </summary>
        public string list { get; } = "search";

        /// <summary>
        /// Search for page titles or content matching this value. You can use the search string to invoke special search features, depending on what the wiki's search backend implements.
        /// This parameter is required.
        /// </summary>
        public string srsearch { get; }

        /// <summary>
        /// Which metadata to return.
        /// Values(separate with | or alternative): totalhits, suggestion, rewrittenquery
        /// Default: totalhits|suggestion|rewrittenquery
        /// </summary>
        public Info[] srinfo { get; set; }

        /// <summary>
        /// How many total pages to return.
        /// Default: 10
        /// Max: 50
        /// </summary>
        public int srlimit { get; set; } = 5;

        /// <summary>
        /// Use this value to continue paging (return by query).
        /// Default: 0
        /// </summary>
        public int sroffset { get; set; }

        /// <summary>
        /// The namespace(s) to enumerate.
        /// When the list is empty, it implicitly contains 0, the default namespace to search.
        /// </summary>
        public int[] srnamespace { get; set; } 

        /// <summary>
        /// What propery to include in the results.
        /// Defaults to a combination of snippet, size, wordcount and timestamp
        /// </summary>
        public Property[] srprop { get; set; }

        /// <summary>
        /// Include redirect pages in the search.
        /// </summary>
        public bool? srredirects { get; set; }

        /// <summary>
        /// Gets or sets the place to search.
        /// </summary>
        public What srwhat { get; set; } = What.Text;
    }


    /// <summary>
    /// Which metadata to return.
    /// </summary>
    public enum Info
    {
        /// <summary>
        /// The number of search results
        /// </summary>
        TotalHits,

        /// <summary>
        /// A suggestion that might fit better than what you searched for.
        /// </summary>
        Suggestion,

        /// <summary>
        /// rewrittenquery
        /// </summary>
        Rewrittenquery
    }

    /// <summary>
    /// What
    /// </summary>
    public enum What
    {
        /// <summary>
        /// Search in page titles (default) (if search engine doesn't support title searches, such as Lucene which is used by Wikipedia, then it falls back to text)
        /// </summary>
        Title,

        /// <summary>
        /// Search in page text
        /// </summary>
        Text,

        /// <summary>
        /// Search for titles that match excatly.
        /// Example:
        /// 'Microsoft' results in 'Microsoft'
        /// 'Microsof' results in 'no results'
        /// </summary>
        NearMatch
    }

    /// <summary>
    /// Property
    /// </summary>
    public enum Property
    {
        /// <summary>
        /// Adds the size of the page in bytes
        /// </summary>
        Size,
        /// <summary>
        /// Adds the word count of the page
        /// </summary>
        Wordcount,
        /// <summary>
        ///  Adds the timestamp of when the page was last edited
        /// </summary>
        Timestamp,
        /// <summary>
        /// Adds the score (if any) from the search engine
        /// </summary>
        Score,
        /// <summary>
        /// Adds a parsed snippet of the page
        /// </summary>
        Snippet,
        /// <summary>
        /// Adds a parsed snippet of the page title
        /// </summary>
        TitleSnippet,
        /// <summary>
        /// Adds a parsed snippet of the redirect
        /// </summary>
        RedirectSnippet,
        /// <summary>
        ///  Adds a parsed snippet of the redirect title
        /// </summary>
        RedirectTitle,
        /// <summary>
        /// Adds a parsed snippet of the matching section
        /// </summary>
        SectionSnippet,
        /// <summary>
        /// Adds a parsed snippet of the matching section title
        /// </summary>
        SectionTitle,
        /// <summary>
        /// Indicates whether a related search is available
        /// </summary>
        HasRelated
    }
}
