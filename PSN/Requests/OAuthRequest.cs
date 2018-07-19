using PSNSharp.Exceptions;
using PSNSharp.Extensions;

namespace PSNSharp.Requests
{
    /// <summary>
    /// Builds the requests for the OAuth tokens.
    /// </summary>
    public static class OAuthRequest
    {
        /// <summary>
        /// Request params to grab OAuth tokens using account credentials.
        /// </summary>
        private class AuthorizationBearer
        {
            public readonly string AppContext = "inapp_ios";
            public readonly string ClientId = "b7cbf451-6bb6-4a5a-8913-71e61f462787";
            public readonly string ClientSecret = "zsISsjmCx85zgCJg";
            public string Code { get; set; }
            public readonly string Duid = "0000000d000400808F4B3AA3301B4945B2E3636E38C0DDFC";
            public readonly string GrantType = "authorization_code";
            public readonly string Scope = "capone:report_submission,psn:sceapp,user:account.get,user:account.settings.privacy.get,user:account.settings.privacy.update,user:account.realName.get,user:account.realName.update,kamaji:get_account_hash,kamaji:ugc:distributor,oauth:manage_device_usercodes";
        }

        /// <summary>
        /// Request params to grab new OAuth tokens using a refresh token.
        /// </summary>
        private class Refresh : AuthorizationBearer
        {
            public new string GrantType = "refresh_token";
            public string RefreshToken { get; set; }
        }

        /// <summary>
        /// Makes the reqeust for OAuth tokens.
        /// </summary>
        /// <param name="grant">The grant type obtained in NPGrantCodeRequest</param>
        /// <returns>Instance of OAuthTokens with the authorization and refresh tokens.</returns>
        public static OAuthTokens Make(string grant)
        {
            if (string.IsNullOrEmpty(grant))
                throw new OAuthTokenNotFoundException("No grant code was supplied");

            AuthorizationBearer ab = new AuthorizationBearer();

			var result = Request.SendPostRequest<dynamic>(APIEndpoints.OAUTH_URL,
			new
			{
				app_context = ab.AppContext,
				client_id = ab.ClientId,
				client_secret = ab.ClientSecret,
				code = grant,
				duid = ab.Duid,
				grant_type = ab.GrantType,
				scope = ab.Scope
			});

            return new OAuthTokens()
            {
                Authorization = result.access_token,
                Refresh = result.refresh_token
            };
        }

        /// <summary>
        /// Makes the request for new OAuth tokens using a refresh token.
        /// </summary>
        /// <param name="refreshToken">Refresh OAuth token for the account.</param>
        /// <returns>Instance of OAuthTokens with the authorization and refresh tokens.</returns>
        public static OAuthTokens MakeNewTokens(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new OAuthTokenNotFoundException("No refresh token was supplied");

            Refresh r = new Refresh();

			// TODO: Why aren't these properties static??
			var result = Request.SendPostRequest<dynamic>(APIEndpoints.OAUTH_URL,
			new
			{
				app_context = r.AppContext,
				client_id = r.ClientId,
				client_secret = r.ClientSecret,
				refresh_token = refreshToken,
				duid = r.Duid,
				grant_type = r.GrantType,
				scope = r.Scope
			});

            return new OAuthTokens()
            {
                Authorization = result.access_token,
                Refresh = result.refresh_token
            };
        }
    }
}