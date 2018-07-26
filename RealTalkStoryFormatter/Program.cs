using RealTalkEngine.StorySystem;
using System;
using System.IO;
using Twinary.StorySystem;

namespace RealTalkStoryFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool completed = false;
            while (!completed)
            {
                Console.WriteLine("Input story file...");

                string input = Console.ReadLine();
                if (input == "q" || input == "Q")
                {
                    completed = true;
                }
                else if (File.Exists(input))
                {
                    // Process story file
                    TwineStory twineStory = TwineStory.Load(input);
                    Story story = Story.Load(twineStory);

                    string outputPath = Path.ChangeExtension(input, "bst");

                    if (story.Save(outputPath, true))
                    {
                        Console.WriteLine("Story saved to " + outputPath);
                    }
                    else
                    {
                        Console.WriteLine("Story failed to save correctly");
                    }
                }
                else
                {
                    Console.WriteLine("Unrecognized input...");
                }
            }
        }
    }
}