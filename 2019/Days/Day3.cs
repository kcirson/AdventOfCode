using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class Day3
    {
        private static List<int> Input =>
            InputHelper.GetInput(2019, 3)[0].Split(',').Select(int.Parse).ToList();

        public static void Run()
        {
            Console.WriteLine("Part 1:");
            Console.WriteLine(Part1());
            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine(Part2());
        }

        private static int Part1()
        {
            return 0;
        }

        private static int Part2()
        {
            return 0;
        }
    }
}
