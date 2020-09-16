using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Common.Helper
{
    public class HttpHelper
    {

        /// <summary>
        /// 不做catch处理，需要在外部做
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method">默认GET，空则补充为GET</param>
        /// <param name="contenttype">默认json，空则补充为json</param>
        /// <param name="header">请求头部</param>
        /// <param name="data">请求body内容</param>
        /// <returns></returns>
        public static string Http(string url, string method = "GET", string data = null, string contenttype = "application/json;charset=utf-8", Hashtable header = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 1000 * 10;
                request.Method = string.IsNullOrEmpty(method) ? "GET" : method;
                request.ContentType = string.IsNullOrEmpty(contenttype) ? "application/json;charset=utf-8" : contenttype;
                if (header != null)
                {
                    foreach (var i in header.Keys)
                    {
                        request.Headers.Add(i.ToString(), header[i].ToString());
                    }
                }
                if (!string.IsNullOrEmpty(data))
                {
                    Stream RequestStream = request.GetRequestStream();
                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                    RequestStream.Write(bytes, 0, bytes.Length);
                    RequestStream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream ResponseStream = response.GetResponseStream();
                StreamReader StreamReader = new StreamReader(ResponseStream, Encoding.GetEncoding("utf-8"));
                string re = StreamReader.ReadToEnd();
                StreamReader.Close();
                ResponseStream.Close();
                return re;
            }
            catch (System.Exception ex)
            {
                return "{\"Code\":\"error\",\"Message\":\"" + ex.Message + "\"}";
            }
        }
    }
}
