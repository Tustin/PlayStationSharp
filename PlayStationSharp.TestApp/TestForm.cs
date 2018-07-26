using System;
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
			TokenHandler.Write(account.Client.Tokens);

			SetupLogin(false);
			PopulateFields();
		}

		private void PopulateFields()
		{
			lblOnlineId.Text = Account.OnlineId;
			lstFriends.DataSource = Account.Friends.Online();
			lblFriends.Text = $"Friends ({lstFriends.Items.Count})";
		}

		private void SetupLogin(bool needsLogin)
		{
			btnLogin.Visible = needsLogin;

			lblOnlineId.Visible = !needsLogin;
			lstFriends.Visible = !needsLogin;
			lblFriends.Visible = !needsLogin;
		}

		private void TestForm_Load(object sender, EventArgs e)
		{
			try
			{
				Account = TokenHandler.Check();
				SetupLogin(false);
				PopulateFields();
			}
			catch (Exception)
			{
				SetupLogin(true);
			}
		}

		private void lstFriends_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (lstFriends.SelectedItem == null) return;

			new ProfileForm(lstFriends.SelectedItem as User).ShowDialog();
		}
	}
}
