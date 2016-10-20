using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaWikiNET.Models
{
    /// <summary>
    /// see at https://en.wikipedia.org/w/api.php?action=help
    /// </summary>
    public class MainModuleRequest:Request
    {
        /// <inheritdoc />
        public override Dictionary<string, string> ToDictionary()
        {
            var lst = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(action)) lst.Add("action", action);
            if (!string.IsNullOrEmpty(format)) lst.Add("format", format);
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
        /// Which action to perform.
        /// One of the following values: abusefiltercheckmatch, abusefilterchecksyntax, abusefilterevalexpression, abusefilterunblockautopromote, addstudents, antispoof, block, bouncehandler, categorytree, centralauthtoken, centralnoticechoicedata, centralnoticequerycampaign, changeauthenticationdata, checktoken, cirrus-config-dump, cirrus-mapping-dump, cirrus-settings-dump, clearhasmsg, clientlogin, compare, createaccount, cspreport, cxconfiguration, cxdelete, cxpublish, cxsave, cxsuggestionlist, cxtoken, delete, deleteeducation, deleteglobalaccount, echomarkread, echomarkseen, edit, editmassmessagelist, emailuser, enlist, expandtemplates, fancycaptchareload, featuredfeed, feedcontributions, feedrecentchanges, feedwatchlist, filerevert, flagconfig, flow, flow-parsoid-utils, flowthank, globalblock, globaluserrights, graph, help, imagerotate, import, jsonconfig, languagesearch, linkaccount, liststudents, login, logout, managetags, massmessage, mergehistory, mobileview, move, opensearch, options, pagetriageaction, pagetriagelist, pagetriagestats, pagetriagetagging, pagetriagetemplate, paraminfo, parse, parsoid-batch, patrol, protect, purge, query, refresheducation, removeauthenticationdata, resetpassword, review, reviewactivity, revisiondelete, rollback, rsd, sanitize-mapdata, scribunto-console, setglobalaccountstatus, setnotificationtimestamp, shortenurl, sitematrix, spamblacklist, stabilize, stashedit, strikevote, tag, templatedata, thank, titleblacklist, tokens, transcodereset, ulslocalization, unblock, undelete, unlinkaccount, upload, userrights, visualeditor, visualeditoredit, watch, wikilove, zeroconfig
        /// Default: help
        /// </summary>
        public virtual string action { get; set; }

        /// <summary>
        /// The format of the output.
        /// One of the following values: json, jsonfm, none, php, phpfm, rawfm, xml, xmlfm
        /// Default: json
        /// </summary>
        public virtual string format { get; set; }

        /// <summary>
        /// Maximum lag can be used when MediaWiki is installed on a database replicated cluster. To save actions causing any more site replication lag, this parameter can make the client wait until the replication lag is less than the specified value. In case of excessive lag, error code maxlag is returned with a message like Waiting for $host: $lag seconds lagged.
        /// See https://www.mediawiki.org/wiki/Manual:Maxlag_parameter for more information.
        /// </summary>
        public int maxlag { get; set; }

        /// <summary>
        /// Set the s-maxage HTTP cache control header to this many seconds. Errors are never cached.
        /// </summary>
        public int smaxage { get; set; }

        /// <summary>
        /// Set the max-age HTTP cache control header to this many seconds. Errors are never cached.
        /// </summary>
        public int maxage { get; set; }
        /// <summary>
        /// Verify the user is logged in if set to user, or has the bot user right if bot.
        /// One of the following values: user, bot
        /// </summary>
        public string assert { get; set; }
        /// <summary>
        /// Verify the current user is the named user.
        /// </summary>
        public string assertuser { get; set; }
        /// <summary>
        /// Any value given here will be included in the response. May be used to distinguish requests.
        /// </summary>
        public string requestid { get; set; }
        /// <summary>
        /// Include the hostname that served the request in the results.
        /// </summary>
        public bool? servedby { get; set; }
        /// <summary>
        /// Include the current timestamp in the result.
        /// </summary>
        public bool? curtimestamp { get; set; }

        /// <summary>
        /// When accessing the API using a cross-domain AJAX request (CORS), set this to the originating domain. This must be included in any pre-flight request, and therefore must be part of the request URI (not the POST body).
        /// For authenticated requests, this must match one of the origins in the Origin header exactly, so it has to be set to something like https://en.wikipedia.org or https://meta.wikimedia.org. If this parameter does not match the Origin header, a 403 response will be returned. If this parameter matches the Origin header and the origin is whitelisted, the Access-Control-Allow-Origin and Access-Control-Allow-Credentials headers will be set.
        /// For non-authenticated requests, specify the value *. This will cause the Access-Control-Allow-Origin header to be set, but Access-Control-Allow-Credentials will be false and all user-specific data will be restricted.
        /// </summary>
        public string origin { get; set; }

        /// <summary>
        /// Language to use for message translations. action=query&amp;meta=siteinfo with siprop=languages returns a list of language codes, or specify user to use the current user's language preference, or specify content to use this wiki's content language.
        /// </summary>
        public string uselang { get; set; }

        /// <summary>
        /// When accessing the API using a cross-domain AJAX request (CORS), use this to authenticate as the current SUL user. Use action=centralauthtoken on this wiki to retrieve the token, before making the CORS request. Each token may only be used once, and expires after 10 seconds. This should be included in any pre-flight request, and therefore should be included in the request URI (not the POST body).
        /// </summary>
        public string centralauthtoken { get; set; }
    }
}
