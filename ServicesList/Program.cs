using System;
using System.IO;
using System.ServiceProcess;
using System.Text;

namespace ServicesList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var svs = ServiceController.GetServices();
            var builder = new StringBuilder();

            foreach (var item in svs)
            {
                builder.AppendLine($"{item.DisplayName},{item.ServiceName},{item.Status.ToString()},{item.StartType.ToString()}");
            }
            Console.WriteLine("select save path");
            string path = Console.ReadLine();
            File.WriteAllText(path, builder.ToString());
        }
    }
}
