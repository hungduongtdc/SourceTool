using System;
using System.Collections.Concurrent;

namespace IgnoreCaseStringComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ConcurrentDictionary<string, string> lstBanks = new ConcurrentDictionary<string, string>(comparer: IgnoreCaseStringComparer.Instance());

            lstBanks.GetOrAdd("aa", "1");
            lstBanks.GetOrAdd("Aa", "1");
            lstBanks.GetOrAdd("Aa", "1");
            lstBanks.GetOrAdd("aA", "1");
            lstBanks.GetOrAdd("AA", "1");
            lstBanks.GetOrAdd("", "1");
            lstBanks.GetOrAdd(" ", "1");

            foreach (var item in lstBanks)
            {
                Console.WriteLine($"{item.Key},{item.Value}");
            }
            Console.ReadLine();
        }
    }
}
