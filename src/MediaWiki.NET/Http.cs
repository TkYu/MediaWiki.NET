using System;
using System.Collections.Generic;
using System.IO;
#if !NET20
using System.Linq;
#endif
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace MediaWikiNET
{
    internal static class Http
    {
        internal static string Get(string url,int timeout = 10000)
        {
#if NET20 || NET40
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "GET";
                httpWebRequest.Timeout = timeout;
                using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    var rs = httpWebResponse.GetResponseStream();
                    if (rs == null) throw new Exception("服务器没有返回任何内容");
                    using (var streamReader = new StreamReader(rs))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
#else
            using (var hc = new System.Net.Http.HttpClient())
            {
                hc.Timeout = TimeSpan.FromMilliseconds(timeout);
                var t = hc.GetByteArrayAsync(url);
                t.Wait(timeout);
                if (!t.IsCompleted)
                    throw new TimeoutException("Request Time Out");
                var data = t.Result;
                return Encoding.UTF8.GetString(data);
            }
#endif
        }

        internal static string Get(string url, IEnumerable<KeyValuePair<string, string>> kv, int timeout = 10000)
        {
            string param;
#if NET20
            var buffer = new StringBuilder();
            foreach (var p in kv)
            {
                buffer.Append($"&{p.Key}={p.Value}");
            }
            param = buffer.ToString().Remove(0,1);
#else
            param = string.Join("&", kv.Select(c => $"{c.Key}={c.Value}"));
#endif
            return Get(url + "?" + param,timeout);
        }

        internal static string PostJson(string url, KeyValuePair<string, string>[] kv, int timeout = 10000)
        {
            var lst = new Dictionary<string, string>();
            if (kv != null && kv.Length > 0)
            {
                foreach (var keyValuePair in kv)
                    lst.Add(keyValuePair.Key, keyValuePair.Value);
            }
            var requestBody = lst.Count > 0 ? JsonConvert.SerializeObject(lst) : "";
            return PostJson(url, requestBody, timeout);
        }
        internal static string PostJson(string url, string requestBody, int timeout = 10000)
        {
#if NET20 || NET40
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = timeout;
            var btBodys = Encoding.UTF8.GetBytes(requestBody);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);
            using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                var rs = httpWebResponse.GetResponseStream();
                if (rs == null) throw new Exception("服务器没有返回任何内容");
                using (var streamReader = new StreamReader(rs))
                {
                    return streamReader.ReadToEnd();
                }
            }
#else
            using (var hc = new System.Net.Http.HttpClient())
            {
                var t1 = hc.PostAsync(url, new System.Net.Http.StringContent(requestBody, Encoding.UTF8, "application/json"));
                t1.Wait(timeout);
                if (!t1.IsCompleted)
                    throw new TimeoutException("请求超时");
                var data = t1.Result;
                var t2 = data.Content.ReadAsByteArrayAsync();
                t2.Wait(timeout);
                if (!t2.IsCompleted)
                    throw new TimeoutException("请求超时");
                return Encoding.UTF8.GetString(t2.Result);
            }
#endif
        }
        internal static string Post(string url, IEnumerable<KeyValuePair<string, string>> kv, int timeout = 10000)
        {
#if NET20 || NET40
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = timeout;
            var buffer = new StringBuilder();
            foreach (var p in kv)
            {
                buffer.Append($"&{p.Key}={p.Value}");
            }
            var btBodys = Encoding.UTF8.GetBytes(buffer.ToString().Remove(0,1));
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);
            using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                var rs = httpWebResponse.GetResponseStream();
                if (rs == null) throw new Exception("服务器没有返回任何内容");
                using (var streamReader = new StreamReader(rs))
                {
                    return streamReader.ReadToEnd();
                }
            }
#else
            using (var hc = new System.Net.Http.HttpClient())
            {
                var t1 = hc.PostAsync(url, new System.Net.Http.FormUrlEncodedContent(kv));
                t1.Wait(timeout);
                if (!t1.IsCompleted)
                    throw new TimeoutException("请求超时");
                var data = t1.Result;
                var t2 = data.Content.ReadAsByteArrayAsync();
                t2.Wait(timeout);
                if (!t2.IsCompleted)
                    throw new TimeoutException("请求超时");
                return Encoding.UTF8.GetString(t2.Result);
            }
#endif
        }


#if !NET20 && !NET40
        internal static async System.Threading.Tasks.Task<string> GetAsync(string url, IEnumerable<KeyValuePair<string, string>> kv, int timeout = 10000)
        {
            return await GetAsync(url + "?" + string.Join("&", kv.Select(c => $"{c.Key}={c.Value}")), timeout);
        }
        internal static async System.Threading.Tasks.Task<string> GetAsync(string url, int timeout = 10000)
        {
            using (var hc = new System.Net.Http.HttpClient())
            {
                hc.Timeout = TimeSpan.FromMilliseconds(timeout);
                var data = await hc.GetByteArrayAsync(url);
                return Encoding.UTF8.GetString(data);
            }
        }

        internal static async System.Threading.Tasks.Task<string> PostJsonAsync(string url, KeyValuePair<string, string>[] kv, int timeout = 10000)
        {
            var lst = new Dictionary<string, string>();
            if (kv != null && kv.Length > 0)
            {
                foreach (var keyValuePair in kv)
                    lst.Add(keyValuePair.Key, keyValuePair.Value);
            }
            var requestBody = lst.Count > 0 ? JsonConvert.SerializeObject(lst) : "";
            return await PostJsonAsync(url, requestBody, timeout);
        }

        internal static async System.Threading.Tasks.Task<string> PostJsonAsync(string url, string requestBody, int timeout = 10000)
        {
            using (var hc = new System.Net.Http.HttpClient())
            {
                hc.Timeout = TimeSpan.FromMilliseconds(timeout);
                var data = await hc.PostAsync(url, new System.Net.Http.StringContent(requestBody, Encoding.UTF8, "application/json"));
                return Encoding.UTF8.GetString(await data.Content.ReadAsByteArrayAsync());
            }
        }
#endif
    }
}
