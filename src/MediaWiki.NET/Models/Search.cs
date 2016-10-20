using System;

namespace MediaWikiNET.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Search
    {
        /// <summary>
        /// 
        /// </summary>
        public int ns { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string snippet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string titlesnippet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string redirecttitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string redirectsnippet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sectiontitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sectionsnippet { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int wordcount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime timestamp { get; set; }

        //[Obsolete("Deprecated and ignored.")]
        //public string score { get; set; }

        //[Obsolete("Deprecated and ignored.")]
        //public string hasrelated { get; set; }
    }
}
