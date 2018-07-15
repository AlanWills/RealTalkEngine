using RealTalkEngine.StorySystem;
using System;
using System.Diagnostics;
using Twinary.StorySystem;

namespace RealTalkProfiling
{
    class Program
    {
        static void Main(string[] args)
        {
            const int iterationCount = 10000;
            const string jsonPath = @"C:\Repos\Dispatcher\RealTalkEngine\Resources\Json Stories\FragmentsOfSkullMaster.json";
            const string binPath = @"C:\Repos\Dispatcher\RealTalkEngine\Resources\Binary Stories\FragmentsOfSkullMaster.data";

            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < iterationCount; ++i)
            {
                TwineStory twineStory = TwineStory.Load(jsonPath);
                Story story = Story.Load(twineStory);
            }
            stopwatch.Stop();

            Console.WriteLine("Json total milliseconds for " + iterationCount + " iterations: " + stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Json average milliseconds: " + (stopwatch.ElapsedMilliseconds / (float)iterationCount));

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < iterationCount; ++i)
            {
                Story story = Story.Load(binPath);
            }
            stopwatch.Stop();

            Console.WriteLine("Bin total milliseconds for " + iterationCount + " iterations: " + stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Bin average milliseconds: " + (stopwatch.ElapsedMilliseconds / (float)iterationCount));
            //Console.ReadKey();
        }
    }
}
