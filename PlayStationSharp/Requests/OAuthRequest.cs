using PlayStationSharp.Exceptions.Auth;
using PlayStationSharp.Extensions;

namespace PlayStationSharp.Requests
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
            public readonly string ClientId = "ebee17ac-99fd-487c-9b1e-18ef50c39ab5";
            public readonly string ClientSecret = "e4Ru_s*LrL4_B2BD";
            public string Code { get; set; }
            public readonly string Duid = "0000000d000400808F4B3AA3301B4945B2E3636E38C0DDFC";
            public readonly string GrantType = "authorization_code";
            public readonly string Scope = "kamaji:get_players_met kamaji:get_account_hash kamaji:activity_feed_submit_feed_story kamaji:activity_feed_internal_feed_submit_story kamaji:activity_feed_get_news_feed kamaji:communities kamaji:game_list kamaji:ugc:distributor oauth:manage_device_usercodes psn:sceapp user:account.profile.get user:account.attributes.validate user:account.settings.privacy.get kamaji:activity_feed_set_feed_privacy kamaji:satchel kamaji:satchel_delete user:account.profile.update";
	        public readonly string RedirectUri = "com.playstation.PlayStationApp://redirect";

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
				scope = ab.Scope,
				redirect_uri = ab.RedirectUri
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