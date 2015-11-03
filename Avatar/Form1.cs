using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avatar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<string> avatars;
        private int avatarIndex = 0;

        private async void btnGet_Click(object sender, EventArgs e)
        {
            const string imageFolder = @"C:\Temp";
            var twitterAction = new TwitterAction(txtConsumerKey.Text, txtConsumerSecret.Text, txtAccessToken.Text, txtTokenSecret.Text);
            var username = txtUsername.Text;
            var userFolder = Path.Combine(imageFolder, username);

            //Disable all buttons
            btnGet.Enabled = false;
            btnPrev.Enabled = false;
            btnNext.Enabled = false;

            await twitterAction.DownloadFriendAvatarsAsync(username, userFolder);

            //Reset the list of avatar and the position of the picture in the list
            avatars = Directory.GetFiles(userFolder).ToList();
            avatarIndex = 0;
            ShowAvatar(0);

            //Enable buttons again
            btnGet.Enabled = true;
            btnPrev.Enabled = true;
            btnNext.Enabled = true;
        }

        private void ShowAvatar(int index)
        {
            pixAvatar.ImageLocation = avatars[index];
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (avatars != null && avatars.Count > 0 && avatarIndex > 0)
            {
                avatarIndex--;
                ShowAvatar(avatarIndex);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (avatars != null && avatars.Count > 0 && avatarIndex < avatars.Count)
            {
                avatarIndex++;
                ShowAvatar(avatarIndex);
            }
        }
    }
}
