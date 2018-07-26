namespace PlayStationSharp.TestApp
{
	partial class ProfileForm
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
			this.imgAvatar = new System.Windows.Forms.PictureBox();
			this.lblOnlineId = new System.Windows.Forms.Label();
			this.txtAboutMe = new System.Windows.Forms.RichTextBox();
			((System.ComponentModel.ISupportInitialize)(this.imgAvatar)).BeginInit();
			this.SuspendLayout();
			// 
			// imgAvatar
			// 
			this.imgAvatar.Location = new System.Drawing.Point(12, 12);
			this.imgAvatar.Name = "imgAvatar";
			this.imgAvatar.Size = new System.Drawing.Size(169, 179);
			this.imgAvatar.TabIndex = 0;
			this.imgAvatar.TabStop = false;
			// 
			// lblOnlineId
			// 
			this.lblOnlineId.AutoSize = true;
			this.lblOnlineId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblOnlineId.Location = new System.Drawing.Point(12, 194);
			this.lblOnlineId.Name = "lblOnlineId";
			this.lblOnlineId.Size = new System.Drawing.Size(51, 20);
			this.lblOnlineId.TabIndex = 1;
			this.lblOnlineId.Text = "label1";
			// 
			// txtAboutMe
			// 
			this.txtAboutMe.BackColor = System.Drawing.SystemColors.Control;
			this.txtAboutMe.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtAboutMe.Location = new System.Drawing.Point(202, 12);
			this.txtAboutMe.Name = "txtAboutMe";
			this.txtAboutMe.ReadOnly = true;
			this.txtAboutMe.Size = new System.Drawing.Size(448, 85);
			this.txtAboutMe.TabIndex = 2;
			this.txtAboutMe.Text = "";
			// 
			// ProfileForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(659, 450);
			this.Controls.Add(this.txtAboutMe);
			this.Controls.Add(this.lblOnlineId);
			this.Controls.Add(this.imgAvatar);
			this.Name = "ProfileForm";
			this.Text = "ProfileForm";
			this.Load += new System.EventHandler(this.ProfileForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.imgAvatar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox imgAvatar;
		private System.Windows.Forms.Label lblOnlineId;
		private System.Windows.Forms.RichTextBox txtAboutMe;
	}
}