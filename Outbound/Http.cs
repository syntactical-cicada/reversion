using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using TkMiddleware.Helpers;
using TkMiddleware.DataObjects;
using System;

namespace TkMiddleware.Outbound
{
    public class HClient : ApiController
    {
        private JsonHelper jsonHelper = new JsonHelper();
        private Http http;
        private readonly string baseUri = ConfigurationManager.AppSettings["BaseUri"];
        private IDictionary<string, string> outboundHeaders = new Dictionary<string, string>();

        public IHttpActionResult Submit(int version, string entityName, HttpRequestHeaders headers, TkWrapper jsonObject = null)
        {
            HttpResponseMessage result;
            HttpContent content;

            try
            {
                http = new Http(headers);
                string uri = string.Format("{0}/api/v{1}/{2}", baseUri, version, entityName);

                if (jsonObject == null) // GET
                {
                    result = http.Get(uri);
                }
                else // POST
                {                
                    content = jsonHelper.GetContent(jsonObject);
                    result = http.Post(uri, content);
                }
                //return Ok(result); /// good for debugging ///

                if (result.IsSuccessStatusCode)
                {
                    var r = result.Content.ReadAsStringAsync();
                    return Ok(r);             
                }
                else
                {
                    return new System.Web.Http.Results.ResponseMessageResult(Request.CreateErrorResponse(result.StatusCode, new HttpError(result.Content.ReadAsStringAsync().Result)));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }
    }

    internal class Http
    {
        private static HttpClient httpClient;

        internal Http(HttpRequestHeaders headers)
        {
            httpClient = new HttpClient();
            SetHeaders(headers);
        }

        private void SetHeaders(HttpRequestHeaders headers)
        {
            // get all headers from incoming request and add them to our outgoing request
            foreach (var header in headers)
            {
                var value = header.Value.FirstOrDefault();
                var key = header.Key;
                if (key == "Authorization")
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(value);
                }
                else
                {
                    if (value != string.Empty)
                    {
                        if (value.Contains(","))
                        {
                            int index = value.IndexOf(",");
                            value = value.Substring(0, index);
                        }
                        httpClient.DefaultRequestHeaders.Add(key, value);
                    }
                }
            }
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        internal HttpResponseMessage Get(string uri)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = httpClient.GetAsync(uri).Result;
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.ServiceUnavailable;
                response.Content = new StringContent(ex.ToString());
            }
            finally
            {
                httpClient = null;
            }
            return response;
        }
        internal HttpResponseMessage Post(string uri, HttpContent json)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = httpClient.PostAsync(uri, json).Result;
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.ServiceUnavailable;
                response.Content = new StringContent(ex.ToString());
            }
            finally
            {
                httpClient = null;
            }
            return response;            
        }
    }
}