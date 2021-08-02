using System;
using System.IO;
using System.Linq;

namespace SearchTool
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Input directory");
                string directory = Console.ReadLine();
                Console.WriteLine("Input search strings piped");
                string searchStrings = Console.ReadLine();

                Console.WriteLine("Searching");
                var directoriesUseStudentMeta = Directory.GetDirectories(directory).Where(SearchMultiple(searchStrings)).ToArray();

                Console.Clear();
                Console.WriteLine($"Search done the projects has keywords : {searchStrings}");
                foreach (var item in directoriesUseStudentMeta)
                {
                    Console.WriteLine(item);
                }
            } while (true);
        }

        private static Func<string, bool> SearchMultiple(string searchStrings)
        {
            return path =>
            {
                return searchStrings.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.ToLower())
                .All(searchString => Search(searchString)(path));
            };
        }

        private static Func<string, bool> Search(string searchString)
        {
            string[] notSearchFolders = new string[] { "content", ".nuget", "fonts", "bin", "scripts", "lib", "obj", "node_modules", "packages", "packages", ".git", ".vs" };
            return path =>
            {
                if (notSearchFolders.Any(sd =>
                 Path.GetFileName(path).ToLower().EndsWith(sd)
                 || Path.GetFileName(path).ToLower().EndsWith($"{sd}\\")
                ))
                {
                    return false;
                }
                Console.WriteLine($"Path:{path}");
                return Directory.GetFiles(path).Where(c => !new string[] { ".exe", ".dll",".dbml",".edmx",".layout" }.Contains(Path.GetExtension(c).ToLower()))
                .Where(c=>!c.ToLower().EndsWith("designer.cs"))
                .AsParallel().WithDegreeOfParallelism(12)
                .Any(filePath =>
                {
                    var res = File.ReadLines(filePath).Any(c => true == c.ToLower().Contains(searchString));
                    return res;
                })
                || Directory.GetDirectories(path).Where(c => !notSearchFolders.Any(sd =>
                Path.GetFileNameWithoutExtension(c).ToLower().EndsWith(sd)
                || Path.GetFileNameWithoutExtension(c).ToLower().EndsWith($"{sd}\\")
                )
                ).Any(Search(searchString))
                ;
            };
        }
    }
}
