using PlayStationSharp.Extensions;
using System;
using System.Windows.Forms;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Microsoft.Toolkit.Win32.UI.Controls.WinForms;

namespace PlayStationSharp.Forms
{
	public partial class LoginForm : Form
	{
		public string GrantCode { get; private set; }

		private string _loginUrl => "https://id.sonyentertainmentnetwork.com/signin/?service_entity=urn:service-entity:psn&response_type=code&client_id=ebee17ac-99fd-487c-9b1e-18ef50c39ab5&redirect_uri=com.playstation.PlayStationApp://redirect&scope=kamaji%3Aget_players_met%20kamaji%3Aget_account_hash%20kamaji%3Aactivity_feed_submit_feed_story%20kamaji%3Aactivity_feed_internal_feed_submit_story%20kamaji%3Aactivity_feed_get_news_feed%20kamaji%3Acommunities%20kamaji%3Agame_list%20kamaji%3Augc%3Adistributor%20oauth%3Amanage_device_usercodes%20psn%3Asceapp%20user%3Aaccount.profile.get%20user%3Aaccount.attributes.validate%20user%3Aaccount.settings.privacy.get%20kamaji%3Aactivity_feed_set_feed_privacy%20kamaji%3Asatchel%20kamaji%3Asatchel_delete%20user%3Aaccount.profile.update&request_locale=en_US";

		// Okay, so the deal with this stuff is for some reason, the WebView control throws an exception for certain people.
		// Since I couldn't repro the issue, the library will fallback to the built in .NET webBrowser control and use that instead.
		// It's pretty horrible, but it'll have to do until I can find out why the WebView crashes.
		public LoginForm()
		{
			InitializeComponent();
			try
			{
				var webViewLogin = new WebView
				{
					Dock = System.Windows.Forms.DockStyle.Fill,
					Location = new System.Drawing.Point(0, 0),
					MinimumSize = new System.Drawing.Size(20, 20),
					Name = "webLogin",
					Size = new System.Drawing.Size(574, 636),
					Source = new System.Uri(this._loginUrl, System.UriKind.Absolute),
					TabIndex = 0,
					Visible = true
				};
				webViewLogin.NavigationStarting += webViewLogin_NavigationStarting;

				this.Controls.Add(webViewLogin);
			}
			catch (Exception)
			{
				var webBrowserLogin = new WebBrowser
				{
					Dock = System.Windows.Forms.DockStyle.Fill,
					Location = new System.Drawing.Point(0, 0),
					MinimumSize = new System.Drawing.Size(20, 20),
					Name = "webLogin",
					Size = new System.Drawing.Size(574, 636),
					Url = new System.Uri(this._loginUrl, System.UriKind.Absolute),
					TabIndex = 0,
					Visible = true,
					ScriptErrorsSuppressed = true
				};

				webBrowserLogin.Navigated += webBrowserLogin_Navigated;

				this.Controls.Add(webBrowserLogin);
			}
		}

		private void webViewLogin_NavigationStarting(object sender, WebViewControlNavigationStartingEventArgs e)
		{
			var url = Uri.UnescapeDataString(e.Uri.ToString());

			if (!url.StartsWith("https://auth.api.sonyentertainmentnetwork.com/mobile-success.jsp")) return;

			var paramName = "code=";
			var code = url.Remove(0, url.IndexOf(paramName, StringComparison.Ordinal) + paramName.Length);
			GrantCode = code.Substring(0, code.IndexOf("&", StringComparison.Ordinal));
			this.Close();
		}

		private void webBrowserLogin_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			var url = Uri.UnescapeDataString(e.Url.ToString());

			if (!url.ToString().StartsWith("https://auth.api.sonyentertainmentnetwork.com/mobile-success.jsp")) return;

			var paramName = "code=";
			var code = url.Remove(0, url.IndexOf(paramName, StringComparison.Ordinal) + paramName.Length);
			GrantCode = code.Substring(0, code.IndexOf("&", StringComparison.Ordinal));
			this.Close();
		}
	}
}