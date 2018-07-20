using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
			var login = Auth.CreateLogin();

			if (login == null) return;

			btnLogin.Enabled = false;
			lblOnlineId.Visible = true;
			lblOnlineId.Text = login.Profile.Information.OnlineId;

		}

		private void TestForm_Load(object sender, EventArgs e)
		{

		}
	}
}
