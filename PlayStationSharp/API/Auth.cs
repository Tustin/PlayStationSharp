using System;
using System.Windows.Forms;
using PlayStationSharp.Extensions;
using PlayStationSharp.Forms;
using PlayStationSharp.Requests;

namespace PlayStationSharp.API
{
	public class Auth
	{
		/// <summary>
		/// Logs into PSN using an email and password.
		/// </summary>
		/// <param name="email">The email address for the account.</param>
		/// <param name="password">The password for the account.</param>
		/// <returns>An instance of the Account class for your account.</returns>
		[Obsolete("Please use Account.CreateLogin() to login without errors.")]
		public static Account Login(string email, string password)
		{
			string npsso = LoginRequest.Create(email, password);
			string grantCode = NpGrantCodeRequest.Make(npsso);
			OAuthTokens tokens = OAuthRequest.Make(grantCode);
			Account a = new Account(tokens);
			return a;
		}

		public static Account CreateLogin()
		{
			var dialog = new LoginForm();

			var tokens = new OAuthTokens();

			dialog.FormClosing += delegate (object sender, FormClosingEventArgs args)
			{
				var grant = dialog.GrantCode;
				if (grant == null) return;

				tokens = OAuthRequest.Make(grant);
			};

			dialog.ShowDialog();

			return tokens == default(OAuthTokens) ? null : new Account(tokens);
		}

		/// <summary>
		/// Verifies the two-step authentication code. Should be used after the initial Login() method.
		/// </summary>
		/// <param name="code">The code sent to the device.</param>
		/// <param name="ticketUuid">The UUID sent back from the Login() method.</param>
		/// <returns></returns>
		[Obsolete("Please use Account.CreateLogin() to login without errors.")]
		public static Account DualAuthLogin(string code, string ticketUuid)
		{
			string npsso = DualAuthLoginRequest.Make(code, ticketUuid);
			string grantCode = NpGrantCodeRequest.Make(npsso);
			OAuthTokens tokens = OAuthRequest.Make(grantCode);
			Account a = new Account(tokens);
			return a;
		}

		/// <summary>
		/// Logs into PSN using a refresh token.
		/// </summary>
		/// <param name="refreshToken">The refresh token for the account.</param>
		/// <returns>An instance of the Account class for your account.</returns>
		public static Account Login(string refreshToken)
		{
			var tokens = OAuthRequest.MakeNewTokens(refreshToken);
			return new Account(tokens);
		}
	}
}