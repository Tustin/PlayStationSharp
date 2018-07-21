using System;
using System.Windows.Forms;
using PlayStationSharp.Exceptions.Auth;
using PlayStationSharp.Extensions;
using PlayStationSharp.Forms;
using PlayStationSharp.Model;

namespace PlayStationSharp.API
{
	public class Auth
	{
		public static class AuthorizationBearer
		{
			public const string AppContext = "inapp_ios";
			public const string ClientId = "ebee17ac-99fd-487c-9b1e-18ef50c39ab5";
			public const string ClientSecret = "e4Ru_s*LrL4_B2BD";
			public static string Code { get; set; }
			public const string Duid = "0000000d000400808F4B3AA3301B4945B2E3636E38C0DDFC";
			public const string GrantType = "authorization_code";
			public const string Scope = "kamaji:get_players_met kamaji:get_account_hash kamaji:activity_feed_submit_feed_story kamaji:activity_feed_internal_feed_submit_story kamaji:activity_feed_get_news_feed kamaji:communities kamaji:game_list kamaji:ugc:distributor oauth:manage_device_usercodes psn:sceapp user:account.profile.get user:account.attributes.validate user:account.settings.privacy.get kamaji:activity_feed_set_feed_privacy kamaji:satchel kamaji:satchel_delete user:account.profile.update";
			public const string RedirectUri = "com.playstation.PlayStationApp://redirect";

		}
		public static Account CreateLogin()
		{
			var dialog = new LoginForm();

			OAuthTokens tokens = null;

			dialog.FormClosing += delegate (object sender, FormClosingEventArgs args)
			{
				var grant = dialog.GrantCode;

				if (grant == null) return;

				try
				{
					var result = Request.SendPostRequest<OAuthTokenModel>(APIEndpoints.OAUTH_URL,
						new
						{
							app_context = AuthorizationBearer.AppContext,
							client_id = AuthorizationBearer.ClientId,
							client_secret = AuthorizationBearer.ClientSecret,
							code = grant,
							duid = AuthorizationBearer.Duid,
							grant_type = AuthorizationBearer.GrantType,
							scope = AuthorizationBearer.Scope,
							redirect_uri = AuthorizationBearer.RedirectUri
						});

					tokens = new OAuthTokens(result);
				}
				catch (GenericAuthException ex)
				{
					switch (ex.Error.ErrorCode)
					{
						case 4159:
							throw new InvalidRefreshTokenException(ex.Error.ErrorDescription);
						default:
							throw;
					}
				}

			};

			dialog.ShowDialog();

			return tokens == default(OAuthTokens) ? null : new Account(tokens);
		}

		public static Account Login(string refreshToken)
		{
			var tokens = new OAuthTokens(refreshToken);
			return new Account(tokens);
		}
	}
}