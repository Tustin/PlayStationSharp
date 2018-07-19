using System;
using System.Net.Http;
using Flurl.Http;

namespace PlayStationSharp
{
	public static class Request
	{
		/// <summary>
		/// Sends a GET request with optional arguments, headers and cookies.
		/// </summary>
		/// <param name="url">The URL of the service to be requested.</param>
		/// <param name="oAuthToken">The Authorization Bearer for the service if it requires authentication (optional).</param>
		/// <param name="data">The GET data for the service.</param>
		/// <param name="headers">Headers to be sent with the request to the service (optional).</param>
		/// <param name="cookies">Cookies to be sent in the header with the request to the service (optional).</param>
		/// <returns>HttpResponseMessage object to be read.</returns>
		public static T SendGetRequest<T>(string url, string oAuthToken = "", object data = null, object headers = null, object cookies = null) where T : class
		{
			try
			{
				return url
					.WithOAuthBearerToken(oAuthToken)
					.GetAsync()
					.ReceiveJson<T>().Result;
			}
			catch (FlurlHttpException)
			{
				throw;
			}


			//if (ContainsKey(response.Content, "error"))
			//	throw new Exception(response.error.message);
			//IFlurlClient fc = SetupRequest(HttpMethod.Get, url, oAuthToken, data, headers, cookies);

			//return fc.GetAsync();
		}

		/// <summary>
		/// Sends a normal POST request with headers and cookies (if supplied).
		/// </summary>
		/// <param name="url">The URL of the service to be requested.</param>
		/// <param name="data">The POST data for the service.</param>
		/// <param name="oAuthToken">The Authorization Bearer for the service if it requires authentication (optional).</param>
		/// <param name="headers">Headers to be sent with the request to the service (optional).</param>
		/// <param name="cookies">Cookies to be sent in the header with the request to the service (optional).</param>
		/// <returns>HttpResponseMessage object to be read.</returns>
		public static T SendPostRequest<T>(string url, object data, string oAuthToken = "", object headers = null, object cookies = null) where T : class
		{
			try
			{
				return url
					.WithOAuthBearerToken(oAuthToken)
					.PostUrlEncodedAsync(data)
					.ReceiveJson<T>().Result;
			}
			catch (FlurlHttpException)
			{
				throw;
			}

			//IFlurlClient fc = SetupRequest(HttpMethod.Post, url, oAuthToken, data, headers, cookies);

			//return fc.PostUrlEncodedAsync(data);
		}

		/// <summary>
		/// Sends a JSON POST request with headers and cookies (if supplied).
		/// </summary>
		/// <param name="url">The URL of the service to be requested.</param>
		/// <param name="data">The POST data for the service.</param>
		/// <param name="oAuthToken">The Authorization Bearer for the service if it requires authentication (optional).</param>
		/// <param name="headers">Headers to be sent with the request to the service (optional).</param>
		/// <param name="cookies">Cookies to be sent in the header with the request to the service (optional).</param>
		/// <returns>HttpResponseMessage object to be read.</returns>
		public static T SendJsonPostRequestAsync<T>(string url, object data, string oAuthToken = "") where T : class
		{
			try
			{
				return url
					.WithOAuthBearerToken(oAuthToken)
					.PostJsonAsync(data)
					.ReceiveJson<T>().Result;
			}
			catch (AggregateException ae)
			{
				ae.Handle(ex => throw ex);
				throw;
			}
		}

		/// <summary>
		/// Sends a DELETE request with headers and cookies (if supplied).
		/// </summary>
		/// <param name="url">The URL of the service to be requested.</param>
		/// <param name="oAuthToken">The Authorization Bearer for the service if it requires authentication (optional).</param>
		/// <param name="headers">Headers to be sent with the request to the service (optional).</param>
		/// <param name="cookies">Cookies to be sent in the header with the request to the service (optional).</param>
		/// <returns>HttpResponseMessage object to be read.</returns>
		public static T SendDeleteRequest<T>(string url, string oAuthToken = "", object headers = null, object cookies = null) where T : class
		{
			try
			{
				return url
					.WithOAuthBearerToken(oAuthToken)
					.DeleteAsync()
					.ReceiveJson<T>().Result;
			}
			catch (FlurlHttpException)
			{
				throw;
			}

			//IFlurlClient fc = SetupRequest(HttpMethod.Delete, url, oAuthToken, null, headers, cookies);

			//return fc.DeleteAsync();
		}

		/// <summary>
		/// Sends a JSON PUT request with headers and cookies (if supplied).
		/// </summary>
		/// <param name="url">The URL of the service to be requested.</param>
		/// <param name="data">The POST data for the service.</param>
		/// <param name="oAuthToken">The Authorization Bearer for the service if it requires authentication (optional).</param>
		/// <param name="headers">Headers to be sent with the request to the service (optional).</param>
		/// <param name="cookies">Cookies to be sent in the header with the request to the service (optional).</param>
		/// <returns>HttpResponseMessage object to be read.</returns>
		public static T SendPutRequest<T>(string url, HttpContent data, string oAuthToken = "") where T : class
		{
			try
			{
				return url
					.WithOAuthBearerToken(oAuthToken)
					.PutAsync(data)
					.ReceiveJson<T>().Result;
			}
			catch (FlurlHttpException)
			{
				throw;
			}

			//IFlurlClient fc = SetupRequest(HttpMethod.Put, url, oAuthToken, data, headers, cookies);

			////This API shouldn't have a service for PUTting non-JSON data.
			//return fc.PutJsonAsync(data);
		}
	}
}
