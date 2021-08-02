using System;
using System.Collections.Generic;
using System.Text;

namespace IgnoreCaseStringComparer
{
    public class IgnoreCaseStringComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return string.Compare(x, y, true) == 0;
        }

        public int GetHashCode(string obj)
        {
            return 0;
        }

        public static IEqualityComparer<string> Instance()
        {
            return new IgnoreCaseStringComparer();
        }
    }
}
