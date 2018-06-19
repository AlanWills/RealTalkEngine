using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool completed = false;
            while (!completed)
            {
                Console.WriteLine("Input audio file directory...");

                string input = Console.ReadLine();
                if (input == "q" || input == "Q")
                {
                    completed = true;
                }
                else if (Directory.Exists(input))
                {
                    // Process audio files
                    string outputDirectory = Path.Combine(input, "Output");
                    if (Directory.Exists(outputDirectory))
                    {
                        Directory.Delete(outputDirectory, true);
                    }

                    Directory.CreateDirectory(outputDirectory);

                    foreach (string file in Directory.EnumerateFiles(input, "*.mp3"))
                    {
                        string fileName = Path.GetFileName(file);
                        Console.WriteLine("Processing " + fileName);

                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.FileName = "ffmpeg";
                        startInfo.Arguments = "-y -i \"" + file + "\" -ac 2 -codec:a libmp3lame -b:a 48k -ar 16000 \"" + Path.Combine(outputDirectory, fileName) + "\"";
                        startInfo.UseShellExecute = false;
                        startInfo.RedirectStandardOutput = true;
                        startInfo.RedirectStandardError = true;
                        process.StartInfo = startInfo;
                        process.Start();

                        Console.WriteLine(process.StandardOutput.ReadToEnd());
                        Console.WriteLine(process.StandardError.ReadToEnd());

                        process.WaitForExit();
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
