using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Parameters;

namespace TwitterApiForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SetUserCredentials();
        }

        private void SetUserCredentials()
        {
            // *********************
            // 3 ways to use credentials
            //  https://github.com/linvi/tweetinvi/wiki/Credentials
            // *********************
            var consumerKey = "xxx";
            var consumerSecret = "yyy";
            var accessToken = "zzz";
            var accessTokenSecret = "ttt";

            // Applies credentials for the current thread. If used for the first time, set up the ApplicationCredentials
            Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            // When a new thread is created, the default credentials will be the Application Credentials
            Auth.ApplicationCredentials = new TwitterCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        }

        private ITweet currentTweetTrail;

        private void button1_Click(object sender, EventArgs e)
        {
            String sTweet = txtTweet.Text;
            if (String.IsNullOrWhiteSpace(sTweet))
            {
                MessageBox.Show("No text. Please write something to tweet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTweet.Focus();
            }
            else
            {
                if (currentTweetTrail == null)
                {
                    currentTweetTrail = Tweet.PublishTweet(sTweet);
                }
                else
                {
                    currentTweetTrail = Tweet.PublishTweetInReplyTo(sTweet, currentTweetTrail);
                }

                txtTweet.Text = "";
                MessageBox.Show("Tweet published.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTweet.Focus();
            }
        }

        private async void btnGetUser_Click(object sender, EventArgs e)
        {
            String sUserScreenName = txtTweet.Text;
            if (String.IsNullOrWhiteSpace(sUserScreenName))
            {
                MessageBox.Show("Please specify a user screen name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTweet.Focus();
            }
            else
            {
                var rootFolder = @"C:\Temp\Twitter";

                //Make a folder for this user
                var userFolder = Path.Combine(rootFolder, sUserScreenName);

                //Clear up the folder
                if (Directory.Exists(userFolder))
                    Directory.Delete(userFolder, true);

                //Re-create it
                Directory.CreateDirectory(userFolder);

                //Get the current user
                var user = User.GetUserFromScreenName(sUserScreenName);

                //limit to 50 friends
                var friends = await user.GetFriendsAsync();

                //download one avatar at a time
                using (var webClient = new WebClient())
                {
                    foreach (var item in friends)
                    {
                        var url = item.ProfileImageUrl400x400;
                        var fileName = Path.Combine(userFolder, item.ScreenName + ".jpg");
                        await webClient.DownloadFileTaskAsync(url, fileName);
                        picAvatar.ImageLocation = fileName;
                    }
                }

                MessageBox.Show("Avatar download done.", "Success");
            }
        }
    }

    public class TweetAction
    {
        private static readonly int BatchSize = 10;

        private async Task DownloadFileAsync(string url, string fileName)
        {
            using (var webclient = new WebClient())
            {
                await webclient.DownloadFileTaskAsync(url, fileName);
            }
        }

        private bool ScreenNameContainsS(IUser user)
        {
            return user.ScreenName.Contains("s");
        }

        public async Task DownloadFriendAvatars(string userScreenName, string folderPath)
        {
            var user = User.GetUserFromScreenName(userScreenName);

            //top 250 friends only
            // to get all, check out: https://github.com/linvi/tweetinvi/wiki/Friendships
            //var friends = await user.GetFriendsAsync();

            var friends = user.GetFriends(50);

            IEnumerable<IUser> t = user.GetFriends(50);

            //download one avatar at a time


            //Example of some selection process
            //method 1
            var m1 = new List<IUser>();
            for (int i = 0; i < friends.Count(); i++)
            {
                if (friends.ElementAt(i).ScreenName.Contains("s"))
                {
                    m1.Add(friends.ElementAt(i));
                }
            }

            //method 2
            var m2 = friends.Where(foo => foo.ScreenName.Contains("s"));

            //method 2a
            var m2a = friends.Where(foo =>
            {
                bool containsS = foo.ScreenName.Contains("s");
                return containsS;
            });

            //method 2c - first class function
            var m2c = friends.Where(ScreenNameContainsS);

            List<String> m2d = friends.Select(foo => foo.ScreenName).ToList();

            var distinctFriends = friends.Select(foo => foo.ScreenName).Distinct().Where(foo => foo.Contains("s"));


            var userGroups = friends.Select((foo, index) => new
            {
                Group = index / BatchSize,
                TwitterUser = foo
            }).GroupBy(foo => foo.Group).ToList();

            //Folder for this user
            string userFolder = Path.Combine(folderPath, userScreenName);

            //Clear up the folder
            if (Directory.Exists(userFolder))
                Directory.Delete(userFolder, true);

            //Re-create it
            Directory.CreateDirectory(userFolder);

            //Async download one chunk at a time
            foreach (var chunk in userGroups)
            {
                var tasks = chunk.Select(foo => foo.TwitterUser).ToList().Select(foo =>
                {
                    var url = foo.ProfileImageUrl400x400;
                    var fileName = Path.Combine(userFolder, foo.ScreenName, ".jpg");
                    return DownloadFileAsync(url, fileName);
                });

                await Task.WhenAll(tasks);
            }
        }
    }
}
