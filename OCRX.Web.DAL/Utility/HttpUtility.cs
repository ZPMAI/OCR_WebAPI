using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OCRX.Web.DAL.Utility
{
    public class HttpUtility
    {
        private static HttpClient client = new HttpClient();
        public async static Task<string> HttpGet(string url)
        {
            try
            {

                //client.BaseAddress = new Uri(uri);
                // Add an Accept header for JSON format. 

                HttpResponseMessage response = client.GetAsync(url).Result;

                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async static Task<string> HttpPost(string uri, string apiname, string data)
        {
            try
            {

                //client.BaseAddress = new Uri(uri);
                // Add an Accept header for JSON format. 
                var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
                string requesturi = string.Format("{0}{1}", uri, apiname);
                HttpResponseMessage response = client.PostAsync(requesturi, stringContent).Result;

                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<string> HttpPost2(string url, string data)
        {
            try
            {
                //SetCertificatePolicy();
                //client.BaseAddress = new Uri(uri);
                // Add an Accept header for JSON format. 
                var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
                string requesturi = url;
                HttpResponseMessage response = client.PostAsync(requesturi, stringContent).Result;

                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<string> HttpECTPost(string uri, string apiname, string data)
        {
            try
            {

                //client.BaseAddress = new Uri(uri);
                // Add an Accept header for JSON format. 
                var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
                string requesturi = string.Format("{0}{1}", uri, apiname);
                client.DefaultRequestHeaders.Add("X-Requested-By", "eshipping");
                HttpResponseMessage response = client.PostAsync(requesturi, stringContent).Result;

                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<string> HttpECTFormPost(string uri, string apiname, Dictionary<string, string> parameters)
        {
            try
            {
                var form = new MultipartFormDataContent();
                string requesturi = string.Format("{0}{1}", uri, apiname);
                client.DefaultRequestHeaders.Add("X-Requested-By", "eshipping");

                foreach (var param in parameters)
                {
                    var stringContent = new StringContent(param.Value);
                    stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = param.Key
                    };

                    form.Add(stringContent);
                }
                HttpResponseMessage response = client.PostAsync(requesturi, form).Result;

                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<string> HttpPut(string uri, string apiname, string data)
        {
            string requesturi = string.Format("{0}{1}", uri, apiname);

            // Add an Accept header for JSON format. 
            //Uri userUri = null;
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(requesturi, stringContent).Result;

            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }


        public async static Task<string> HttpsPost(string uri, string apiname, string data)
        {
            try
            {
                string requesturi = string.Format("{0}{1}", uri, apiname);
                SetCertificatePolicy();
                // Add an Accept header for JSON format. 
                //Uri userUri = null;
                var stringContent = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(requesturi, stringContent).Result;

                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Sets the cert policy.
        /// </summary>
        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;
        }

        /// <summary>
        /// Remotes the certificate validate.
        /// </summary>
        private static bool RemoteCertificateValidate(
           object sender, X509Certificate cert,
            X509Chain chain, SslPolicyErrors error)
        {
            // trust any certificate!!!
            System.Console.WriteLine("Warning, trust any certificate");
            return true;
        }
    }
}
