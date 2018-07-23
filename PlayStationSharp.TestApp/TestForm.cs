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

		private Account Account { get; set; }

		private void btnLogin_Click(object sender, EventArgs e)
		{
			var account = Auth.CreateLogin();

			if (account == null) return;

			this.Account = account;

			btnLogin.Enabled = false;

			lblOnlineId.Visible = true;
			lblOnlineId.Text = account.OnlineId;

			lstFriends.Visible = true;
			lstFriends.DataSource = account.Friends.Select(a => a.OnlineId).ToList();

			lblFriends.Visible = true;
			lblFriends.Text = $"Friends ({lstFriends.Items.Count})";
		}

		private void TestForm_Load(object sender, EventArgs e)
		{

		}
	}
}
