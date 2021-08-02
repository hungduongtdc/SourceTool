using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CheckoutDevelop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input directory");
            string directory = Console.ReadLine();

            foreach (var item in Directory.GetDirectories(directory))
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WorkingDirectory = item,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false
                };
                Process process = new Process()
                {
                    StartInfo = startInfo
                };
                process.Start();
                process.StandardInput
                    .WriteLine("git checkout develop");
                process.StandardInput
                    .WriteLine("git pull");
            }
        }
    }
}
