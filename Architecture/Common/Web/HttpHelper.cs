using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CCT.Common.Web
{
    public class HttpHelper
    {
        private const int ConnectionLimit = 100;
        //编码
        //private Encoding _encoding = Encoding.Default;
        private Encoding _encoding = Encoding.UTF8;
        //浏览器类型
        private string[] _useragents = new string[]{
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.90 Safari/537.36",
            "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/7.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0)",
            "Mozilla/5.0 (Windows NT 6.1; rv:36.0) Gecko/20100101 Firefox/36.0",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:31.0) Gecko/20130401 Firefox/31.0"
        };

        //private String _useragent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.90 Safari/537.36";
        private String _useragent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.76 Mobile Safari/537.36";

        //接受类型
        //private String _accept = "text/html, application/xhtml+xml, application/xml, */*";
        private String _accept = "text/html,application/xhtml+xml,application/json,application/xml;q=0.9,image/webp,*/*;q=0.8";

        //超时时间
        private int _timeout = 30 * 1000;
        //类型
        private string _contenttype = "application/json";
        //cookies
        private String _cookies = "";
        //cookies
        private CookieCollection _cookiecollection;

        private string _responseUrl = "";

        public string ResponseUrl
        {
            get { return _responseUrl; }
            set { _responseUrl = value; }
        }


        public CookieCollection Cookiecollection
        {
            get { return _cookiecollection; }
            set { _cookiecollection = value; }
        }
        //custom heads
        private Dictionary<string, string> _headers = new Dictionary<string, string>();

        public HttpHelper()
        {
            _headers.Clear();
            //随机一个useragent
            _useragent = _useragents[new Random().Next(0, _useragents.Length)];
            //解决性能问题?
            ServicePointManager.DefaultConnectionLimit = ConnectionLimit;
        }

        public void InitCookie()
        {
            _cookies = "";
            _cookiecollection = null;
            _headers.Clear();
        }

        /// <summary>
        /// 设置当前编码
        /// </summary>
        /// <param name="en"></param>
        public void SetEncoding(Encoding en)
        {
            _encoding = en;
        }

        /// <summary>
        /// 设置UserAgent
        /// </summary>
        /// <param name="ua"></param>
        public void SetUserAgent(String ua)
        {
            _useragent = ua;
        }

        public void RandUserAgent()
        {
            _useragent = _useragents[new Random().Next(0, _useragents.Length)];
        }

        public void SetCookiesString(string c)
        {
            _cookies = c;
        }

        /// <summary>
        /// 设置超时时间
        /// </summary>
        /// <param name="sec"></param>
        public void SetTimeOut(int msec)
        {
            _timeout = msec;
        }

        public void SetContentType(String type)
        {
            _contenttype = type;
        }

        public void SetAccept(String accept)
        {
            _accept = accept;
        }


        public void test()
        {
            CookieContainer cookies = new CookieContainer();

            string url = "http://www.baidu.com/";
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myHttpWebRequest.Timeout = 20 * 1000; //连接超时
            myHttpWebRequest.Accept = "*/*";
            myHttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0;)";
            myHttpWebRequest.CookieContainer = new CookieContainer(); //暂存到新实例
            var response = myHttpWebRequest.GetResponse();
            var s = response.Headers["Set-Cookie"].ToString();

            response.Close();
            //cookies = myHttpWebRequest.CookieContainer; //保存cookies
            //string cookiesstr = myHttpWebRequest.CookieContainer.GetCookieHeader(myHttpWebRequest.RequestUri); //把cookies转换成字符串

        }
        /// <summary>
        /// 添加自定义头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ctx"></param>
        public void AddHeader(String key, String ctx)
        {
            //_headers.Add(key,ctx);
            _headers[key] = ctx;
        }

        /// <summary>
        /// 清空自定义头
        /// </summary>
        public void ClearHeader()
        {
            _headers.Clear();
        }

        /// <summary>
        /// 获取HTTP返回的内容
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private String GetStringFromResponse(HttpWebResponse response)
        {
            String html = "";
            StreamReader reader = null;
            try
            {
                if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                {
                    //开始读取流并设置编码方式
                    using (reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), _encoding))
                    {

                        html = reader.ReadToEnd();

                    }
                }
                else
                {
                    //开始读取流并设置编码方式
                    using (reader = new StreamReader(response.GetResponseStream(), _encoding))
                    {

                        html = reader.ReadToEnd();
                    }
                }




                ////Stream stream = response.GetResponseStream();
                ////StreamReader sr = new StreamReader(stream, _encoding);
                ////html = sr.ReadToEnd();

                ////sr.Close();
                ////stream.Close();


                //MemoryStream stmMemory = new MemoryStream();
                //byte[] buffer = new byte[64 * 1024];
                //int i;
                //while ((i = stream.Read(buffer, 0, buffer.Length)) > 0)
                //{
                //    stmMemory.Write(buffer, 0, i);
                //}
                //byte[] arraryByte = stmMemory.ToArray();
                //html = _encoding.GetString(arraryByte);
                //stmMemory.Close();
                //stream.Close();

            }
            catch (Exception e)
            {
                Trace.WriteLine("GetStringFromResponse Error: " + e.Message);
            }

            return html;
        }

        /// <summary>
        /// 检测证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private bool CheckCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 获取CooKie
        /// </summary>
        /// <param name="loginUrl"></param>
        /// <param name="postdata"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public CookieContainer GetCooKie(string loginUrl, string postdata)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                CookieContainer cc = new CookieContainer();
                request = (HttpWebRequest)WebRequest.Create(loginUrl);
                request.Method = "GET";
                request.ContentType = _contenttype;
                byte[] postdatabyte = Encoding.UTF8.GetBytes(postdata);
                request.ContentLength = postdatabyte.Length;
                request.AllowAutoRedirect = false;
                request.CookieContainer = cc;
                request.KeepAlive = true;

                //提交请求
                Stream stream;
                stream = request.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                stream.Close();

                //接收响应
                response = (HttpWebResponse)request.GetResponse();
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);

                CookieCollection cook = response.Cookies;
                //Cookie字符串格式
                string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);

                return cc;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 获取CooKie
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string setCooKie(string url)
        {
            string html = string.Empty;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                CookieContainer cc = new CookieContainer();
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = _contenttype;
                request.AllowAutoRedirect = false;
                request.CookieContainer = cc;
                request.KeepAlive = true;

                //接收响应
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                    _cookiecollection = response.Cookies;
                    //_cookies = request.CookieContainer.GetCookieHeader(request.RequestUri);

                    //string addCookies = "CNZZDATA1000500682=775860281-1445566752-%7C1456300554; yunsuo_session_verify=bb82cb7a5543c3bbae9fb120791e0ab7;ePayNoted=true;SurveyNoted=true;VGMNoted=true";
                    //AddCookieWithCookieHead(_cookiecollection, addCookies, "iport.sctcn.com");

                    html = GetStringFromResponse(response);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return html;
        }

        private void AddCookieWithCookieHead(CookieCollection cookieCol, string cookieHead, string defaultDomain)
        {
            if (cookieHead == null) return;
            string[] ary = cookieHead.Split(';');
            for (int i = 0; i < ary.Length; i++)
            {
                Cookie ck = GetCookieFromString(ary[i].Trim(), defaultDomain);
                if (ck != null)
                {
                    cookieCol.Add(ck);
                }
            }
        }

        private Cookie GetCookieFromString(string cookieString, string defaultDomain)
        {
            string[] ary = cookieString.Split(';');
            Hashtable hs = new Hashtable();
            for (int i = 0; i < ary.Length; i++)
            {
                string s = ary[i].Trim();
                int index = s.IndexOf("=");
                if (index > 0)
                {
                    hs.Add(s.Substring(0, index), s.Substring(index + 1));
                }
            }
            Cookie ck = new Cookie();
            foreach (object Key in hs.Keys)
            {
                if (Key.ToString() == "path") ck.Path = hs[Key].ToString();

                else if (Key.ToString() == "expires")
                {
                    //ck.Expires=DateTime.Parse(hs[Key].ToString();
                }
                else if (Key.ToString() == "domain") ck.Domain = hs[Key].ToString();
                else
                {
                    ck.Name = Key.ToString();
                    ck.Value = hs[Key].ToString();
                }
            }
            if (ck.Name == "") return null;
            if (ck.Domain == "") ck.Domain = defaultDomain;
            return ck;
        }



        public string GetCookieString()
        {
            return _cookies;
        }


        /// <summary>
        /// 获取验证码方法 
        /// </summary> 

        public Bitmap LoadImg(string ImageUrl)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ImageUrl);//请求的URL 
                request.UserAgent = _useragent;
                request.Timeout = _timeout;
                request.ContentType = _contenttype;
                request.Accept = _accept;
                request.Method = "GET";
                request.Referer = ImageUrl;
                request.AllowAutoRedirect = true;
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.KeepAlive = true;
                request.CookieContainer = new CookieContainer();
                //据说能提高性能
                request.Proxy = null;
                if (_cookiecollection != null)
                {
                    foreach (Cookie c in _cookiecollection)
                    {
                        c.Domain = request.Address.Host;
                    }

                    request.CookieContainer.Add(_cookiecollection);
                }

                //获取返回资源 
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (request.CookieContainer != null)
                    {
                        response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                    }
                }

                //var s = response.Headers["Set-Cookie"].ToString();
                //CookieContainer cookies = request.CookieContainer; //保存cookies
                //string cookiesstr = request.CookieContainer.GetCookieHeader(request.RequestUri); //把cookies转换成字符串
                //获取流 
                Bitmap bt = Bitmap.FromStream(response.GetResponseStream()) as Bitmap;
                return bt;

            }
            catch (Exception ex)
            {
                //return null;
            }
            return null;

        }

        /// <summary>
        /// 发送GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public String HttpGet(String url, Hashtable ht = null)
        {
            String html;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckCertificate);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.UserAgent = _useragent;
                request.Timeout = _timeout;
                request.ContentType = _contenttype;
                request.Accept = _accept;
                request.Method = "GET";
                request.Referer = url.Substring(0, url.IndexOf('?')>0? url.IndexOf('?'):url.Length);
                request.KeepAlive = true;
                request.AllowAutoRedirect = true;
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.CookieContainer = new CookieContainer();
                //据说能提高性能
                request.Proxy = null;
                if (_cookiecollection != null)
                {
                    foreach (Cookie c in _cookiecollection)
                    {
                        c.Domain = request.Address.Host;
                    }

                    request.CookieContainer.Add(_cookiecollection);
                }

                foreach (KeyValuePair<String, String> hd in _headers)
                {
                    request.Headers[hd.Key] = hd.Value;
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                html = GetStringFromResponse(response);
                if (request.CookieContainer != null)
                {
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                }

                if (response.Cookies != null)
                {
                    _cookiecollection = response.Cookies;
                }
                if (response.Headers["Set-Cookie"] != null)
                {
                    string tmpcookie = response.Headers["Set-Cookie"];
                    _cookiecollection.Add(ConvertCookieString(tmpcookie));
                }

                response.Close();
                return html;
            }
            catch (Exception e)
            {
                Trace.WriteLine("HttpGet Error: " + e.Message);
                return String.Empty;
            }
        }


        /// <summary>
        /// 发送GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="refer"></param>
        /// <returns></returns>
        public String HttpGet2(String url, String refer)
        {
            String html;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckCertificate);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.UserAgent = _useragent;
                request.Timeout = _timeout;
                request.ContentType = _contenttype;
                request.Accept = _accept;
                request.Method = "GET";
                request.Referer = refer;
                request.KeepAlive = true;
                request.AllowAutoRedirect = true;
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.CookieContainer = new CookieContainer();
                //据说能提高性能
                request.Proxy = null;
                if (_cookiecollection != null)
                {
                    foreach (Cookie c in _cookiecollection)
                    {
                        c.Domain = request.Address.Host;
                    }

                    request.CookieContainer.Add(_cookiecollection);
                }

                foreach (KeyValuePair<String, String> hd in _headers)
                {
                    request.Headers[hd.Key] = hd.Value;
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                html = GetStringFromResponse(response);
                if (request.CookieContainer != null)
                {
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                }

                if (response.Cookies != null)
                {
                    _cookiecollection = response.Cookies;
                }
                if (response.Headers["Set-Cookie"] != null)
                {
                    string tmpcookie = response.Headers["Set-Cookie"];
                    _cookiecollection.Add(ConvertCookieString(tmpcookie));
                }

                response.Close();
                return html;
            }
            catch (Exception e)
            {
                Trace.WriteLine("HttpGet Error: " + e.Message);
                return String.Empty;
            }
        }

        public static string HttpGetRequest(string requestUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            //设置连接超时时间 
            request.Timeout = 30000;
            request.KeepAlive = true;
            Encoding encodeType = Encoding.GetEncoding("UTF-8");
            request.Headers.Set("Pragma", "no-cache");
            request.Method = "GET";

            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; Maxthon; .NET CLR 1.1.4322); Http STdns";
            request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";

            string strb = string.Empty;
            //接收数据
            using (Stream reader = request.GetResponse().GetResponseStream())
            {
                StreamReader sr = new StreamReader(reader, encodeType);
                strb = sr.ReadToEnd();
                sr.Close();
                reader.Close();
            }
            return strb;
        }

        /// <summary>
        /// 获取MINE文件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Byte[] HttpGetMine(String url)
        {
            Byte[] mine = null;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckCertificate);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.UserAgent = _useragent;
                request.Timeout = _timeout;
                request.ContentType = _contenttype;
                request.Accept = _accept;
                request.Method = "GET";
                request.Referer = url;
                request.KeepAlive = true;
                request.AllowAutoRedirect = true;
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.CookieContainer = new CookieContainer();
                //据说能提高性能
                request.Proxy = null;
                if (_cookiecollection != null)
                {
                    foreach (Cookie c in _cookiecollection)
                        c.Domain = request.Address.Host;
                    request.CookieContainer.Add(_cookiecollection);
                }

                foreach (KeyValuePair<String, String> hd in _headers)
                {
                    request.Headers[hd.Key] = hd.Value;
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                MemoryStream ms = new MemoryStream();

                byte[] b = new byte[1024];
                while (true)
                {
                    int s = stream.Read(b, 0, b.Length);
                    ms.Write(b, 0, s);
                    if (s == 0 || s < b.Length)
                    {
                        break;
                    }
                }
                mine = ms.ToArray();
                ms.Close();

                if (request.CookieContainer != null)
                {
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                }

                if (response.Cookies != null)
                {
                    _cookiecollection = response.Cookies;
                }
                if (response.Headers["Set-Cookie"] != null)
                {
                    _cookies = response.Headers["Set-Cookie"];
                }

                stream.Close();
                stream.Dispose();
                response.Close();
                return mine;
            }
            catch (Exception e)
            {
                Trace.WriteLine("HttpGetMine Error: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public String HttpPost(String url, String data)
        {
            return HttpPost(url, data, url);
        }


        public string GetContent(string url)
        {
            url = "http://iport.sctcn.com/?U_token=NcVE8Z0q8yFS0Tb8dgy.1k8NF90oDSqYlrvhEG89XEQ/LfF1Bgsi3g==&P_token=lBpDep2Anuzp4qzzI9SjZ3FXki0om95WZcIACV52a9sDNLuzz.LZtLAw0Cok9OzX&UserName=SCT&LoginTime=2016-08-17 11:14:22";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.AllowAutoRedirect = false;
            request.Referer = url;

            request.CookieContainer = new CookieContainer();
            //据说能提高性能
            request.Proxy = null;
            if (_cookiecollection != null)
            {
                foreach (Cookie c in _cookiecollection)
                {
                    c.Domain = request.Address.Host;
                }

                request.CookieContainer.Add(_cookiecollection);
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return "";
            }

            StreamReader reader = new StreamReader(response.GetResponseStream(), _encoding);
            string html = reader.ReadToEnd();
            return html;
        }

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="refer"></param>
        /// <returns></returns>
        public String HttpPost(String url, String data, String refer)
        {
            String html;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckCertificate);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.UserAgent = _useragent;
                request.Timeout = _timeout;
                //request.Referer = refer;
                //request.Referer = "http://iport.sctcn.com/";
                request.ContentType = _contenttype;
                request.Accept = _accept;
                request.Method = "POST";
                request.KeepAlive = true;
                //request.AllowAutoRedirect = true;
                request.AllowAutoRedirect = false;
                request.CookieContainer = new CookieContainer();
                request.MaximumAutomaticRedirections = 2;

                request.Headers["Accept-Encoding"] = "gzip, deflate";
                request.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                request.Headers["Cache-Control"] = "max-age=0";
                // request.Headers["Connection"] = "keep-alive";
                //request.Headers["Content-Length"] = "291";
                //request.Headers["Origin"] = "http://iport.sctcn.com";
                //request.Headers["Referer"] = "http://iport.sctcn.com";
                //request.Headers["Upgrade-Insecure-Requests"] = "1";

                //MyRequest.Method = "POST";
                //MyRequest.ContentLength = postdata.Length;
                //MyRequest.CookieContainer = Cookies;
                //MyRequest.KeepAlive = true;
                //MyRequest.AllowAutoRedirect = true;
                //MyRequest.ContentType = "application/x-www-form-urlencoded";
                //MyRequest.UserAgent = "    Mozilla/5.0 (Windows NT 5.1; rv:14.0) Gecko/20100101 Firefox/14.0.1";
                //MyRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

                //据说能提高性能
                request.Proxy = null;

                if (_cookiecollection != null)
                {
                    foreach (Cookie c in _cookiecollection)
                        c.Domain = request.Address.Host;
                    request.CookieContainer.Add(_cookiecollection);
                }

                foreach (KeyValuePair<String, String> hd in _headers)
                {
                    request.Headers[hd.Key] = hd.Value;
                }

                //byte[] buffer = _encoding.GetBytes(data.Trim());
                //ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] buffer = _encoding.GetBytes(data.Trim());

                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                //var json = new JavaScriptSerializer().DeserializeObject(data);
                //if (json == null)
                //{

                //}
                //using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                //{
                //    string json = new JavaScriptSerializer().Serialize(new
                //    {
                //        method = "login",
                //        auth = new { apikey = "eport", apisecret = "eportpwd" },
                //        param = new { username = "lb002", password = "123456" }
                //    });

                //    streamWriter.Write(json);
                //}

                request.GetRequestStream().Close();

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;  

                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    ArrayList list = new ArrayList();
                    WebResponse wr = ex.Response;
                    using (Stream st = wr.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(st, System.Text.Encoding.Default))
                        {
                            list.Add(sr.ReadToEnd());
                        }
                    }
                }



                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Found)
                {
                    return "";
                }

                if (request.CookieContainer != null)
                {
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                }
                if (response.Cookies != null)
                {
                    _cookiecollection = response.Cookies;
                }
                if (response.Headers["Set-Cookie"] != null)
                {
                    string tmpcookie = response.Headers["Set-Cookie"];
                    _cookiecollection.Add(ConvertCookieString(tmpcookie));
                }

                if (response.StatusCode == HttpStatusCode.Found)
                {
                    string rediUrl = request.Referer + response.Headers["Location"];
                    response.Close();
                    return GetContent(rediUrl);
                }

                //response.Headers["Location"] = response.ResponseUri.PathAndQuery;
                html = GetStringFromResponse(response);
                _responseUrl = response.ResponseUri.ToString();

                response.Close();
                return html;
            }
            catch (Exception e)
            {

                Trace.WriteLine("HttpPost Error: " + e.Message);
                return String.Empty;
            }
        }


        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="refer"></param>
        /// <returns></returns>
        public String HttpPut(String url, String data)
        {
            String html;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckCertificate);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.UserAgent = _useragent;
                request.Timeout = _timeout;
                request.ContentType = _contenttype;
                request.Accept = _accept;
                request.Method = "PUT";
                request.KeepAlive = true;
                //request.AllowAutoRedirect = true;
                request.AllowAutoRedirect = false;
                request.CookieContainer = new CookieContainer();
                request.MaximumAutomaticRedirections = 2;

                //if (ht != null && ht.Count > 0)
                //{
                //    foreach (DictionaryEntry de in ht){
                //        request.Headers[de.Key.ToString()] = de.Value.ToString();
                //    } 

                //}

                //据说能提高性能
                request.Proxy = null;

                if (_cookiecollection != null)
                {
                    foreach (Cookie c in _cookiecollection)
                        c.Domain = request.Address.Host;
                    request.CookieContainer.Add(_cookiecollection);
                }

                foreach (KeyValuePair<String, String> hd in _headers)
                {
                    request.Headers[hd.Key] = hd.Value;
                }

                //byte[] buffer = _encoding.GetBytes(data.Trim());
                //ASCIIEncoding encoding = new ASCIIEncoding();
                if (data != null && data.Length > 0)
                {
                    byte[] buffer = _encoding.GetBytes(data.Trim());

                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                    request.GetRequestStream().Close();

                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;  
                }
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    ArrayList list = new ArrayList();
                    WebResponse wr = ex.Response;
                    using (Stream st = wr.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(st, System.Text.Encoding.Default))
                        {
                            list.Add(sr.ReadToEnd());
                        }
                    }
                }



                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Found)
                {
                    return "";
                }

                if (request.CookieContainer != null)
                {
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                }
                if (response.Cookies != null)
                {
                    _cookiecollection = response.Cookies;
                }
                if (response.Headers["Set-Cookie"] != null)
                {
                    string tmpcookie = response.Headers["Set-Cookie"];
                    _cookiecollection.Add(ConvertCookieString(tmpcookie));
                }

                if (response.StatusCode == HttpStatusCode.Found)
                {
                    string rediUrl = request.Referer + response.Headers["Location"];
                    response.Close();
                    return GetContent(rediUrl);
                }

                //response.Headers["Location"] = response.ResponseUri.PathAndQuery;
                html = GetStringFromResponse(response);
                _responseUrl = response.ResponseUri.ToString();

                response.Close();
                return html;
            }
            catch (Exception e)
            {
                Trace.WriteLine("HttpPost Error: " + e.Message);
                return String.Empty;
            }
        }


        public string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = _encoding.GetBytes(str);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }

        /// <summary>
        /// 转换cookie字符串到CookieCollection
        /// </summary>
        /// <param name="ck"></param>
        /// <returns></returns>
        private CookieCollection ConvertCookieString(string ck)
        {
            CookieCollection cc = new CookieCollection();
            string[] cookiesarray = ck.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < cookiesarray.Length; i++)
            {
                string[] cookiesarray_2 = cookiesarray[i].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < cookiesarray_2.Length; j++)
                {
                    string[] cookiesarray_3 = cookiesarray_2[j].Trim().Split("=".ToCharArray());
                    if (cookiesarray_3.Length == 2)
                    {
                        string cname = cookiesarray_3[0].Trim();
                        string cvalue = cookiesarray_3[1].Trim();
                        if (cname != "domain" && cname != "path" && cname != "expires")
                        {
                            Cookie c = new Cookie(cname, cvalue);
                            cc.Add(c);
                        }
                    }
                }
            }

            return cc;
        }


        public void DebugCookies()
        {
            Trace.WriteLine("**********************BEGIN COOKIES*************************");
            foreach (Cookie c in _cookiecollection)
            {
                Trace.WriteLine(c.Name + "=" + c.Value);
                Trace.WriteLine("Path=" + c.Path);
                Trace.WriteLine("Domain=" + c.Domain);
            }
            Trace.WriteLine("**********************END COOKIES*************************");
        }

    }
}
