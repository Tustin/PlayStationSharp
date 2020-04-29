using System;
using System.Windows.Forms;
using PlayStationSharp.API;

namespace PlayStationSharp.TestApp
{
	public partial class ProfileForm : Form
	{
		private readonly User _user;

		public ProfileForm(User user)
		{
			InitializeComponent();
			_user = user;

			var sesh = user.Session;

			lblOnlineId.Text = _user.OnlineId;
			txtAboutMe.Text = _user.Profile.AboutMe;
			imgAvatar.ImageLocation = _user.Profile.AvatarUrls[0].AvatarUrl;
			this.Text = _user.OnlineId;
		}

		private void ProfileForm_Load(object sender, EventArgs e)
		{

		}
	}
}
