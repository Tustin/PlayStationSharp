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
	public partial class ProfileForm : Form
	{
		private readonly User _user;

		public ProfileForm(User user)
		{
			InitializeComponent();
			_user = user;

			lblOnlineId.Text = _user.OnlineId;
			txtAboutMe.Text = _user.Profile.AboutMe;
			imgAvatar.ImageLocation = _user.Profile.AvatarUrls[0].AvatarUrl;
		}

		private void ProfileForm_Load(object sender, EventArgs e)
		{

		}
	}
}
