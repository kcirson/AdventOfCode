using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    class Day5
    {
        private static List<string> Input =>
                InputHelper.GetInputString(2020, 5).Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.None).ToList();

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
