using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioConstsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // C:\Users\alawi\Source\Repos\RealTalkGames\EmergencyResponderGame\Dispatcher\Resources\Audio\Birth Call
            // https://s3-eu-west-1.amazonaws.com/rtg-dispatcher/birth-call/

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
                    Console.WriteLine("Input root url...");
                    string rootUrl = Console.ReadLine();

                    DirectoryInfo directory = new DirectoryInfo(input);  
                    string directoryClass = directory.Name.Replace(" ", "");
                    string outputFile = Path.Combine(input, directoryClass + "AudioConsts.cs");

                    StringBuilder builder = new StringBuilder(1024);
                    builder.AppendLine("using System;");
                    builder.AppendLine("using System.Collections.Generic;");
                    builder.AppendLine("using System.IO;");
                    builder.AppendLine("using System.Linq;");
                    builder.AppendLine("using System.Text;");
                    builder.AppendLine("using System.Threading.Tasks;");
                    builder.Append("\nclass ");
                    builder.Append(directoryClass);
                    builder.AppendLine("AudioConsts\n{");

                    foreach (FileInfo file in directory.EnumerateFiles("*.mp3"))
                    {
                        builder.Append("\tpublic const string ");
                        builder.Append(Path.GetFileNameWithoutExtension(file.Name).Replace(' ', '_'));
                        builder.Append(" = \"");
                        builder.Append(rootUrl);
                        builder.Append(file.Name);
                        builder.AppendLine("\";");
                    }

                    builder.Append("}");
                    
                    File.WriteAllText(outputFile, builder.ToString());
                    Console.WriteLine("Consts file generated");
                }
                else
                {
                    Console.WriteLine("Unrecognized input...");
                }
            }
        }
    }
}
