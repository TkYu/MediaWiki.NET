using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaWikiNET;
using MediaWikiNET.Models;

namespace sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var wiki = new MediaWiki();
            var result = wiki.Search("HelloWorld");
            foreach (var search in result.query.search)
            {
                Console.WriteLine(search.title);
                Console.WriteLine(search.snippet);
                Console.WriteLine(search.timestamp);
            }
            Console.WriteLine();
            wiki.Language = Languages.Chinese;
            result = wiki.SearchAsync(new SearchRequest("我爱你") {srlimit = 3}).GetAwaiter().GetResult();
            foreach (var search in result.query.search)
            {
                Console.WriteLine(search.title);
                Console.WriteLine(search.snippet);
                Console.WriteLine(search.timestamp);
            }
        }
    }
}
