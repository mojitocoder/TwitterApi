using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Credentials;

namespace Avatar
{
    public class TwitterAction
    {
        private string consumerKey;
        private string consumerSecret;
        private string accessToken;
        private string accessTokenSecret;

        public TwitterAction(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.accessToken = accessToken;
            this.accessTokenSecret = accessTokenSecret;

            SetUserCredentials();
        }

        private void SetUserCredentials()
        {
            // *********************
            // 3 ways to use credentials
            //  https://github.com/linvi/tweetinvi/wiki/Credentials
            // *********************

            // Applies credentials for the current thread. If used for the first time, set up the ApplicationCredentials
            Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            // When a new thread is created, the default credentials will be the Application Credentials
            Auth.ApplicationCredentials = new TwitterCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        }

        public async Task DownloadFriendAvatarsAsync(string username, string folderPath)
        {
            //make sure that this folder exists and empty
            if (Directory.Exists(folderPath))
            {
                //delete everything inside
                Directory.GetDirectories(folderPath).ToList().ForEach(foo => Directory.Delete(foo, true));
                Directory.GetFiles(folderPath).ToList().ForEach(foo => File.Delete(foo));
            }
            else Directory.CreateDirectory(folderPath);

            var user = User.GetUserFromScreenName(username);
            var friends = await user.GetFriendsAsync();

            using (var webClient = new WebClient())
            {
                foreach (var friend in friends)
                {
                    var url = friend.ProfileImageUrl400x400;
                    var filePath = Path.Combine(folderPath, friend.ScreenName + ".jpg");
                    await webClient.DownloadFileTaskAsync(url, filePath);
                }
            }
        }
    }
}
