using System.Collections.Generic;
using System.Dynamic;

namespace PSNSharp.Extensions
{
	/// <summary>
	/// Utilities used throughout the project
	/// </summary>
	public static class Utilities
	{
		/// <summary>
		/// Setups a request for the SendRequest methods in this class.
		/// </summary>
		/// <param name="method">The HTTP method.</param>
		/// <param name="url">The URL of the service to be requested.</param>
		/// <param name="oAuthToken">The Authorization Bearer for the service if it requires authentication (optional).</param>
		/// <param name="data">The GET data for the service.</param>
		/// <param name="headers">Headers to be sent with the request to the service (optional).</param>
		/// <param name="cookies">Cookies to be sent in the header with the request to the service (optional).</param>
		/// <returns>FlurlClient object to be used to send the request.</returns>
		//private static IFlurlClient SetupRequest(HttpMethod method, string url, string oAuthToken = "", object data = null, object headers = null, object cookies = null)
		//{
		//	if (method == HttpMethod.Get && data != null)
		//		url = url.SetQueryParams(data);

		//	IFlurlClient fc = new FlurlClient(url);

		//	if (headers != null)
		//		fc = fc.WithHeaders(headers);
		//	if (cookies != null)
		//		fc = fc.WithCookies(cookies);
		//	if (!string.IsNullOrEmpty(oAuthToken))
		//		fc = fc.WithHeader("Authorization", new AuthenticationHeaderValue("Bearer", oAuthToken));

		//	return fc.AllowAnyHttpStatus();
		//}

		/// <summary>
		/// Sends a request.
		/// </summary>
		/// <param name="method">The HttpMethod to use.</param>
		/// <param name="flurlClient">The FlurlClient object to use for the request.</param>
		/// <param name="content">HttpContent object of the data to be sent.</param>
		/// <returns>HttpResponseMessage object to be read.</returns>
		//public static Task<HttpResponseMessage> SendRequest(HttpMethod method, FlurlClient flurlClient, HttpContent content)
		//{
		//	return flurlClient.SendAsync(method, content);
		//}



		/// <summary>
		/// Checks if a dynamic ExpandoObject contains a key and is not null.
		/// </summary>
		/// <param name="o">The ExpandoObject to check</param>
		/// <param name="key">The key to check</param>
		/// <returns>True if key exists, false if otherwise.</returns>
		public static bool ContainsKey(this ExpandoObject o, string key)
		{
			return ((o != null) && ((IDictionary<string, object>)o).ContainsKey(key));
		}
	}
}