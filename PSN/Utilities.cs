using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Flurl.Http;
using Flurl;
using System.Dynamic;

namespace PSN.Extensions
{
    /// <summary>
    /// Utilities used throughout the project
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Sends a GET request with optional arguments, headers and cookies.
        /// </summary>
        /// <param name="URL">The URL of the service to be requested.</param>
        /// <param name="data">The GET data for the service.</param>
        /// <param name="headers">Headers to be sent with the request to the service (optional).</param>
        /// <param name="cookies">Cookies to be sent in the header with the request to the service (optional).</param>
        /// <returns>HTTResponseMessage object to be read.</returns>
        public static async Task<HttpResponseMessage> SendGETRequest(string URL, object data = null, object headers = null, object cookies = null)
        {
            Url fc = new Url(URL);
            if (data != null)
                fc.SetQueryParams(data);
            if (headers != null)
                fc.WithHeaders(headers);
            if (cookies != null)
                fc.WithCookies(cookies);
            return await URL.GetAsync();
        }

        /// <summary>
        /// Sends a normal POST request with headers and cookies (if supplied).
        /// </summary>
        /// <param name="URL">The URL of the service to be requested.</param>
        /// <param name="data">The POST data for the service.</param>
        /// <param name="headers">Headers to be sent with the request to the service (optional).</param>
        /// <param name="cookies">Cookies to be sent in the header with the request to the service (optional).</param>
        /// <returns>HTTResponseMessage object to be read.</returns>
        public static async Task<HttpResponseMessage> SendPOSTRequest(string URL, object data, object headers = null, object cookies = null)
        {
            FlurlClient fc = URL.AllowAnyHttpStatus();
            if (headers != null)
                fc.WithHeaders(headers);
            if (cookies != null)
                fc.WithCookies(cookies);
            return await fc.PostUrlEncodedAsync(data);
        }
        /// <summary>
        /// Sends a JSON POST request with headers and cookies (if supplied).
        /// </summary>
        /// <param name="URL">The URL of the service to be requested.</param>
        /// <param name="data">The POST data for the service.</param>
        /// <param name="headers">Headers to be sent with the request to the service (optional).</param>
        /// <param name="cookies">Cookies to be sent in the header with the request to the service (optional).</param>
        /// <returns>HTTResponseMessage object to be read.</returns>
        public static async Task<HttpResponseMessage> SendJSONPOSTRequest(string URL, object data, object headers = null, object cookies = null)
        {
            FlurlClient fc = URL.AllowAnyHttpStatus();
            if (headers != null)
                fc.WithHeaders(headers);
            if (cookies != null)
                fc.WithCookies(cookies);
            return await fc.PostJsonAsync(data);
        }

        /// <summary>
        /// Checks if a dynamic ExpandoObject contains a key.
        /// </summary>
        /// <param name="o">The ExpandoObject to check</param>
        /// <param name="key">The key to check</param>
        /// <returns>True if key exists, false if otherwise.</returns>
        public static bool ContainsKey(this ExpandoObject o, string key)
        {
            return ((IDictionary<string, object>)o).ContainsKey(key);
        }
    }
}
