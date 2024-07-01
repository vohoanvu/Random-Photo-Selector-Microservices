using System.Diagnostics;
using System.Windows;

namespace AssigmentOneUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Please enter the command 'start': ");
                string command = Console.ReadLine();

                if (command == "start")
                {
                    string prngFilePath = 
                        @"D:\vu-repos\LocalPersonalDocs\Computer-Science-degree-competion\OSU\CS361\AssigmentOneUI\PRNG-Service\prng-service.txt";
                    string imageServiceFilePath = 
                        @"D:\vu-repos\LocalPersonalDocs\Computer-Science-degree-competion\OSU\CS361\AssigmentOneUI\Image-Service\image-service.txt";

                    File.WriteAllText(prngFilePath, "run");
                    Console.WriteLine("Waiting for PRNG service...");

                    string randomNumber = WaitForPRNGFileUpdate(prngFilePath);
                    Console.WriteLine($"Received pseudo-random number: {randomNumber}");

                    File.WriteAllText(imageServiceFilePath, randomNumber);
                    Console.WriteLine("Waiting for Image service...");

                    string imagePath = WaitForImageFileUpdate(imageServiceFilePath);
                    Console.WriteLine($"Received image path: {imagePath}");

                    string fullPath = imagePath
                        .Replace("~/Image-Service/ImageAssets/", 
                        @"D:\vu-repos\LocalPersonalDocs\Computer-Science-degree-competion\OSU\CS361\AssigmentOneUI\Image-Service\ImageAssets\");
                    Console.WriteLine($"Opening image full file: {fullPath}");

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "powershell",
                        Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"Start-Process '{fullPath}' -Wait\"",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    })?.WaitForExit();

                    Console.WriteLine($"Finishing the AssigmentOne UI program...");
                }
                else if (command == "exit")
                {
                    break;
                }
            }
        }

        private static string WaitForPRNGFileUpdate(string filePath)
        {
            string content = string.Empty;
            string lastContent = string.Empty;
            while (true)
            {
                content = File.ReadAllText(filePath);
                if (int.TryParse(content, out int randomNumber) && content != lastContent)
                {
                    return content;
                }
                lastContent = content;
                Thread.Sleep(2000);
            }
        }

        private static string WaitForImageFileUpdate(string filePath)
        {
            string content = string.Empty;
            string lastContent = string.Empty;
            while (true)
            {
                content = File.ReadAllText(filePath);
                var isFileChanged = content.StartsWith("~/Image-Service/ImageAssets/") 
                    && content.EndsWith(".jpg") 
                    && content != lastContent;
                if (isFileChanged)
                {
                    return content;
                }
                lastContent = content;
                Thread.Sleep(2000);
            }
        }
    }
}
