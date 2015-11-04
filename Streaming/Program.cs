using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces.Streaminvi.Parameters;

namespace Streaming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");

            // *********************
            // Credentials
            // *********************
            string consumerKey = "";
            string consumerSecret = "";
            string accessToken = "";
            string accessTokenSecret = "";

            // *********************
            // 3 ways to use credentials
            //  https://github.com/linvi/tweetinvi/wiki/Credentials
            // *********************

            // Applies credentials for the current thread. If used for the first time, set up the ApplicationCredentials
            Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            // When a new thread is created, the default credentials will be the Application Credentials
            Auth.ApplicationCredentials = new TwitterCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);


            // *********************
            // Setup streams
            // *********************
            var stream = Stream.CreateSampleStream();

            stream.AddTweetLanguageFilter(Language.English);
            stream.FilterLevel = StreamFilterLevel.None;
            stream.StallWarnings = true;

            stream.TweetReceived += (sender, arguments) =>
            {
                Console.WriteLine(arguments.Tweet);
            };
            stream.StartStream();




            Console.ReadLine();
        }
    }
}
