namespace PlayStationSharp.Forms
{
	partial class LoginForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
			this.webLogin = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// webLogin
			// 
			this.webLogin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webLogin.Location = new System.Drawing.Point(0, 0);
			this.webLogin.MinimumSize = new System.Drawing.Size(20, 20);
			this.webLogin.Name = "webLogin";
			this.webLogin.Size = new System.Drawing.Size(574, 636);
			this.webLogin.TabIndex = 0;
			this.webLogin.Url = new System.Uri(resources.GetString("webLogin.Url"), System.UriKind.Absolute);
			this.webLogin.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webLogin_Navigated);
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(574, 636);
			this.Controls.Add(this.webLogin);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Login";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser webLogin;
	}
}