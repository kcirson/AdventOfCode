using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers
{
    public static class ExtensionMethods
    {
        public static List<int> ToIntList(this int integer)
        {
            List<int> individual = new();

            while (integer > 0)
            {
                individual.Add(integer % 10);
                integer /= 10;
            }

            individual.Reverse();

            return individual;
        }
    }
}
