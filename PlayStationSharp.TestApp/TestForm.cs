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
using PlayStationSharp.Extensions;

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

			var user = account.FindUser("tustin25aaa");
			//	.GetMessageThreads()
			//	.PrivateMessageThread();

			//if (thread == null) return; // No private message group found

			//thread.SendMessage("hey there!");

			MessageBox.Show("sent message");

		}

		private void TestForm_Load(object sender, EventArgs e)
		{

		}
	}
}
