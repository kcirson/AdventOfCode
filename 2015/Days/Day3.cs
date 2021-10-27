using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public static class Day3
    {
        private static string Input =>
            InputHelper.GetInputString(2015, 3);
            //"^v^v^v^v^v";

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
            Dictionary<string, Dimension> map = new();
            Dimension dim = new('@', 0, 0);
            map.Add("00", dim);

            foreach (char c in Input)
            {
                dim = new Dimension(c, dim.X, dim.Y);

                if (map.ContainsKey(dim.ToString()))
                    map[dim.ToString()].AmountOfPresents += 1;
                else
                    map.Add(dim.ToString(), dim);
            }

            int housesWithPresents = map.Select(dim => dim.Value.AmountOfPresents > 0).Count();

            return housesWithPresents;
        }

        private static int Part2()
        {
            Dictionary<string, Dimension> map = new();
            Dimension santa = new('@', 0, 0);
            Dimension roboSanta = new('@', 0, 0);
            map.Add(santa.ToString(), santa);

            bool santaGoes = true;

            foreach (char c in Input)
            {
                if (santaGoes)
                {
                    santa = new Dimension(c, santa.X, santa.Y);

                    if (map.ContainsKey(santa.ToString()))
                        map[santa.ToString()].AmountOfPresents += 1;
                    else
                        map.Add(santa.ToString(), santa);
                    
                    santaGoes = !santaGoes;
                }
                else {
                    roboSanta = new Dimension(c, roboSanta.X, roboSanta.Y);

                    if (map.ContainsKey(roboSanta.ToString()))
                        map[roboSanta.ToString()].AmountOfPresents += 1;
                    else
                        map.Add(roboSanta.ToString(), roboSanta);
                    
                    santaGoes = !santaGoes;
                }
            }

            int santaPresents = map.Select(dim => dim.Value.AmountOfPresents > 0).Count();

            return santaPresents;
        }
    }

    public class Dimension
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int AmountOfPresents { get; set; } = 0;

        public Dimension(char c, int prevX, int prevY)
        {
            switch (c)
            {
                case '^':
                    Y = prevY + 1;
                    X = prevX;
                    break;
                case 'v':
                    Y = prevY - 1;
                    X = prevX;
                    break;
                case '<':
                    X = prevX - 1;
                    Y = prevY;
                    break;
                case '>':
                    X = prevX + 1;
                    Y = prevY;
                    break;
                default:
                    X = prevX;
                    Y = prevY;
                    break;
            }

            AmountOfPresents++;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
