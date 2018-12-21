using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Jitsukawa.Extensions.Http
{
    public static class Http
    {
        public static async Task<string> SoapRequestAsync(this HttpClient client, HttpMethod method, string url, string SOAPAction, string xmlSOAP, AuthenticationHeaderValue authentication = null)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.Add("SOAPAction", SOAPAction);
            request.Method = method;
            request.Content = new StringContent(xmlSOAP); ;
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml; charset=utf-8");

            if (authentication != null)
                client.DefaultRequestHeaders.Authorization = authentication;

            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> HtmlRequestAsync(this HttpClient client, string url, long bufferSize = 256000)
        {
            client.MaxResponseContentBufferSize = bufferSize;
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}