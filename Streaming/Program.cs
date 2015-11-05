using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Events.EventArguments;
using Tweetinvi.Core.Interfaces;
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
            var consumerKey = "1IkRIDMoGhwD0uqvoUq1vxtcF";
            var consumerSecret = "xybThYiZs5nViaoXtOQ19Thvp7xf5n0w2vII8Lj93gfX7QMu8f";
            var accessToken = "391233365-ud5RMbWDF4agMjIo63F3ZEsTocpADSH7Tycj0K67";
            var accessTokenSecret = "F4C6LHt0B1YAxCTTtmm2d5zfZgcENmRrPeNGsKbCoedoY";

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

            //stream.TweetReceived += (sender, arguments) =>
            //{
            //    Console.WriteLine(arguments.Tweet);
            //};

            Observable.FromEventPattern<TweetReceivedEventArgs>(stream, "TweetReceived")
                .Select(foo => foo.EventArgs.Tweet) //trim off all the unnecessary elements from each element (i.e. event) in the stream
                .Buffer(TimeSpan.FromSeconds(3))
                .Subscribe(tweets =>
                {
                    var authors = string.Join(", ", tweets.Select(foo => foo.CreatedBy));

                    Console.WriteLine("\n\n{0}: {1} tweets from: {2}", DateTime.UtcNow, tweets.Count, authors);
                });

            stream.StartStream();

            //Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe((x) =>
            //{
            //    Console.WriteLine("Tick");
            //});

            Console.ReadLine();

            stream.StopStream();
        }
    }
}
