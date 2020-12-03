using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class Day2
    {
        private static List<Rules> Input =>
            InputHelper.GetInput(2020, 2).Select(s => new Rules(s)).ToList();
        public static void Run()
        {
            Console.WriteLine("Part 1:");
            Console.WriteLine(Part1());
            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine(Part2());
        }

        private static int Part1() =>
            Input.Count(rule => rule.IsValidPasswordPart1);

        private static int Part2() =>
            Input.Count(rule => rule.IsValidPasswordPart2);

    }

    public class Rules
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public char Required { get; set; }
        public string Password { get; set; }

        public bool IsValidPasswordPart1
        {
            get
            {
                int countRequired = Password.Where(c => c == Required).Count();

                return countRequired <= Max && countRequired >= Min;
            }
        }

        public bool IsValidPasswordPart2
        {
            get
            {
                int MinZeroBased = Min - 1;
                int MaxZeroBased = Max - 1;

                Regex reg1 = new Regex($"^.{{{MinZeroBased}}}[{Required}]");
                Regex reg2 = new Regex($"^.{{{MaxZeroBased}}}[{Required}]");

                Match match1 = reg1.Match(Password);
                Match match2 = reg2.Match(Password);

                if (match1.Success && match2.Success)
                    return false;

                if (match1.Success || match2.Success)
                    return true;

                return false;
            }
        }

        public Rules(string rule)
        {
            string[] split = rule.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] minMaxSplit = split[0].Split('-');

            Min = Convert.ToInt32(minMaxSplit[0]);
            Max = Convert.ToInt32(minMaxSplit[1]);
            Required = Convert.ToChar(split[1].Replace(':', ' ').Trim());
            Password = split[2];
        }
    }
}
