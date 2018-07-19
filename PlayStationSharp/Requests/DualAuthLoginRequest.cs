using System.Security.Authentication;
using Flurl.Http;
using PlayStationSharp.Exceptions;
using PlayStationSharp.Exceptions.Auth;
using PlayStationSharp.Model;

namespace PlayStationSharp.Requests
{
	public class DualAuthLoginRequest
	{
		private static readonly string AuthenticationType = "two_step";
		private static readonly string ClientId = "71a7beb8-f21a-47d9-a604-2e71bee24fe0";

		public static string Make(string code, string ticketUuid)
		{
			if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(code))
				throw new NpssoIdNotFoundException("The code or ticket uuid was not set");

			try
			{
				var result = Request.SendJsonPostRequestAsync<dynamic>(APIEndpoints.SSO_COOKIE_URL, new
				{
					authentication_type = AuthenticationType,
					ticket_uuid = ticketUuid,
					code = code,
					client_id = ClientId
				});

				//if (Utilities.ContainsKey(result, "error"))
				//    throw new NpssoIdNotFoundException(result.error_description);

				return result.npsso;
			}
			catch (NpssoIdNotFoundException e) { throw new NpssoIdNotFoundException(e.Message); }
			catch (FlurlHttpException ex)
			{
				var error = ex.GetResponseJsonAsync<ErrorModel>().Result;

				switch (error.ErrorCode)
				{
					case 4420:
						throw new InvalidTwoStepCredentialsException();
					default:
						throw new GenericAuthException(error.ErrorDescription);
				}
			}
		}
	}
}
