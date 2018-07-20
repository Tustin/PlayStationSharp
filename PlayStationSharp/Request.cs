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
		/// <returns>HttpResponseMessage object to be read.</returns>
		public static T SendGetRequest<T>(string url, string oAuthToken = "", object data = null) where T : class
		{
			try
			{
				return url
					.WithOAuthBearerToken(oAuthToken)
					.GetAsync()
					.ReceiveJson<T>().Result;
			}
			catch (AggregateException ae)
			{
				ae.Handle(ex => throw ex);
				throw;
			}
		}

		/// <summary>
		/// Sends a normal POST request with headers and cookies (if supplied).
		/// </summary>
		/// <param name="url">The URL of the service to be requested.</param>
		/// <param name="data">The POST data for the service.</param>
		/// <param name="oAuthToken">The Authorization Bearer for the service if it requires authentication (optional).</param>
		/// <returns>HttpResponseMessage object to be read.</returns>
		public static T SendPostRequest<T>(string url, object data, string oAuthToken = "") where T : class
		{
			try
			{
				return url
					.WithOAuthBearerToken(oAuthToken)
					.PostUrlEncodedAsync(data)
					.ReceiveJson<T>().Result;
			}
			catch (AggregateException ae)
			{
				ae.Handle(ex => throw ex);
				throw;
			}
		}

		/// <summary>
		/// Sends a JSON POST request with headers and cookies (if supplied).
		/// </summary>
		/// <param name="url">The URL of the service to be requested.</param>
		/// <param name="data">The POST data for the service.</param>
		/// <param name="oAuthToken">The Authorization Bearer for the service if it requires authentication (optional).</param>
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
		/// <returns>HttpResponseMessage object to be read.</returns>
		public static T SendDeleteRequest<T>(string url, string oAuthToken = "") where T : class
		{
			try
			{
				return url
					.WithOAuthBearerToken(oAuthToken)
					.DeleteAsync()
					.ReceiveJson<T>().Result;
			}
			catch (AggregateException ae)
			{
				ae.Handle(ex => throw ex);
				throw;
			}

		}

		/// <summary>
		/// Sends a JSON PUT request with headers and cookies (if supplied).
		/// </summary>
		/// <param name="url">The URL of the service to be requested.</param>
		/// <param name="data">The POST data for the service.</param>
		/// <param name="oAuthToken">The Authorization Bearer for the service if it requires authentication (optional).</param>
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
			catch (AggregateException ae)
			{
				ae.Handle(ex => throw ex);
				throw;
			}
		}
	}
}
