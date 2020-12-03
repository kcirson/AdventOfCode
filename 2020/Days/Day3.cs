using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day3
    {
        public static List<char[]> Input =>
                InputHelper.GetInput(2020, 3).Select(s => s.ToCharArray()).ToList();

        public static void Run()
        {
            Console.WriteLine("Part 1:");
            Console.WriteLine(Part1());
            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine(Part2());
        }

        public static int Part1()
        {
            return CheckTrees(3, 1);
        }

        public static long Part2()
        {
            List<Tuple<int, int>> Slopes = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(3, 1),
                new Tuple<int, int>(5, 1),
                new Tuple<int, int>(7, 1),
                new Tuple<int, int>(1, 2)
            };

            long treeCount = 0;

            Parallel.ForEach(Slopes, slope =>
            {
                int trees = CheckTrees(slope.Item1, slope.Item2);

                if (treeCount == 0)
                    treeCount = trees;
                else
                    treeCount *= trees;
            });

            return treeCount;
        }

        private static int CheckTrees(int rightSteps, int downSteps)
        {
            int count = Input.Count;
            int treeCount = 0;
            int right = rightSteps;
            int down = downSteps;

            while (down < count)
            {
                char[] row = Input[down];

                if (right >= row.Length)
                    right -= row.Length;

                char obj = row[right];

                if (obj == '#')
                    treeCount++;

                right += rightSteps;
                down += downSteps;
            }

            return treeCount;
        }
    }
}
