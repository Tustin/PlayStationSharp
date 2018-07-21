using System;
using System.Net.Http;
using System.Text;
using Flurl.Http;
using Newtonsoft.Json;
using PlayStationSharp.Exceptions;
using PlayStationSharp.Exceptions.Auth;
using PlayStationSharp.Model;

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
				ae.Handle(ex =>
				{
					if (!(ex is FlurlHttpException fe)) throw ex;
					var error = fe.GetResponseJsonAsync<ErrorModel>().Result;
					throw new PlayStationApiException(error.Error);

				});
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
				ae.Handle(ex =>
				{
					// Because Sony doesn't like to be consistent, authentication endpoints seem to return a different error JSON format.
					// Since this method is the only one still used for authentication, we'll only handle the auth errors.
					if (!(ex is FlurlHttpException fe)) throw ex;
					var error = fe.GetResponseJsonAsync<AuthErrorModel>().Result;
					throw new GenericAuthException(error);
				});
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
				ae.Handle(ex =>
				{
					if (!(ex is FlurlHttpException fe)) throw ex;
					var error = fe.GetResponseJsonAsync<ErrorModel>().Result;
					throw new PlayStationApiException(error.Error);

				});
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
				ae.Handle(ex =>
				{
					if (!(ex is FlurlHttpException fe)) throw ex;
					var error = fe.GetResponseJsonAsync<ErrorModel>().Result;
					throw new PlayStationApiException(error.Error);

				});
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
		public static T SendPutRequest<T>(string url, HttpContent data = null, string oAuthToken = "") where T : class
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
				ae.Handle(ex =>
				{
					if (!(ex is FlurlHttpException fe)) throw ex;
					var error = fe.GetResponseJsonAsync<ErrorModel>().Result;
					throw new PlayStationApiException(error.Error);

				});
				throw;
			}
		}

		public static T SendMultiPartPostRequest<T>(string url, string boundry, string dispositionName, object data, string oAuthToken = "") where T : class
		{
			try
			{
				var sb = new StringBuilder();

				sb.Append($"--{boundry}");
				sb.AppendLine();
				sb.Append("Content-Type: application/json; charset=utf-8");
				sb.AppendLine();
				sb.Append($"Content-Disposition: form-data; name=\"{dispositionName}\"");
				sb.AppendLine();
				sb.AppendLine();
				sb.Append(JsonConvert.SerializeObject(data, Formatting.Indented));
				sb.AppendLine();
				sb.Append($"--{boundry}--");

				return url
					.WithOAuthBearerToken(oAuthToken)
					.WithHeader("Content-Type", $"multipart/form-data; boundary=\"{boundry}\"")
					.PostStringAsync(sb.ToString())
					.ReceiveJson<T>().Result;

			}
			catch (AggregateException ae)
			{
				ae.Handle(ex =>
				{
					if (!(ex is FlurlHttpException fe)) throw ex;
					var error = fe.GetResponseJsonAsync<ErrorModel>().Result;
					throw new PlayStationApiException(error.Error);

				});
				throw;
			}
		}
	}


}
