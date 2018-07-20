using System;
using Flurl.Http;
using PlayStationSharp.Exceptions.Auth;
using PlayStationSharp.Extensions;
using PlayStationSharp.Model;

namespace PlayStationSharp.Requests
{
	/// <summary>
	/// Class that builds the initial login request
	/// </summary>
	public static class LoginRequest
	{
		public static readonly string AuthenticationType = "password";
		public static string Username { get; set; }
		public static string Password { get; set; }
		public static readonly string ClientId = "71a7beb8-f21a-47d9-a604-2e71bee24fe0";

		/// <summary>
		/// Execute the initial login request.
		/// </summary>
		/// <returns>NPSSO Id for the next auth step.</returns>
		public static string Create(string username, string password)
		{
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
				throw new ArgumentException("The username or password fields have not been set.");

			try
			{
				var result = Request.SendJsonPostRequestAsync<dynamic>(APIEndpoints.SSO_COOKIE_URL, new
				{
					authentication_type = AuthenticationType,
					username,
					password,
					client_id = ClientId
				});

				// Dual auth is set
				if (Utilities.ContainsKey(result, "authentication_type"))
				{
					// If the user uses SMS verification, throw exception containing the ticket UUID
					// so you can ask the user to supply the code sent to their device.
					if (result.challenge_method == "SMS")
						throw new DualAuthSMSRequiredException(result.ticket_uuid);
				}

				return result.npsso;
			}
			catch (NpssoIdNotFoundException e)
			{
				throw new NpssoIdNotFoundException(e.Message);
			}
			catch (FlurlHttpException ex)
			{
				var error = ex.GetResponseJsonAsync<ErrorModel>().Result;

				switch (error.ErrorCode)
				{
					case 20:
						throw new InvalidCredentialsException();
					case 4097:
						throw new CaptchaTokenMissingException();
					default:
						throw new GenericAuthException(error.ErrorDescription);
				}
			}
		}
	}
}