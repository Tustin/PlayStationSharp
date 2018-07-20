using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlayStationSharp.API;

namespace PlayStationSharp.TestApp
{
	public partial class TestForm : Form
	{
		public TestForm()
		{
			InitializeComponent();
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			var account = Auth.CreateLogin();

			if (account == null) return;

			btnLogin.Enabled = false;
			lblOnlineId.Visible = true;
			lblOnlineId.Text = account.Profile.Information.OnlineId;

			var user = account.FindUser("tustin25");
			var trophies = user.Trophies;
		}

		private void TestForm_Load(object sender, EventArgs e)
		{

		}
	}
}
