using PlayStationSharp.Extensions;
using System;
using System.Windows.Forms;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;

namespace PlayStationSharp.Forms
{
	public partial class LoginForm : Form
	{
		public string GrantCode { get; private set; }

		public LoginForm()
		{
			InitializeComponent();
		}

		private void webLogin_NavigationStarting(object sender, WebViewControlNavigationStartingEventArgs e)
		{
			var url = Uri.UnescapeDataString(e.Uri.ToString());

			if (!url.StartsWith("https://auth.api.sonyentertainmentnetwork.com/mobile-success.jsp")) return;

			var paramName = "code=";
			var code = url.Remove(0, url.IndexOf(paramName, StringComparison.Ordinal) + paramName.Length);
			GrantCode = code.Substring(0, code.IndexOf("&", StringComparison.Ordinal));
			this.Close();
		}
	}
}