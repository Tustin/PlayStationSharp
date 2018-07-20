using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace PlayStationSharp.Forms
{
	public partial class LoginForm : Form
	{
		public LoginForm()
		{
			InitializeComponent();
		}

		public string GrantCode { get; private set; }

		private void webLogin_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			var url = Uri.UnescapeDataString(webLogin.Url.ToString());
			if (!url.ToString().StartsWith("https://auth.api.sonyentertainmentnetwork.com/mobile-success.jsp")) return;

			var paramName = "code=";
			var code = url.Remove(0, url.IndexOf(paramName, StringComparison.Ordinal) + paramName.Length);
			GrantCode = code.Substring(0, code.IndexOf("&", StringComparison.Ordinal));
			this.Close();
		}
	}
}
