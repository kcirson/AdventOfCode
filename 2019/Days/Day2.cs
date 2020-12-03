using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class Day2
    {
        private static List<int> Input =>
            InputHelper.GetInput(2019, 2)[0].Split(',').Select(int.Parse).ToList();

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
            IntProgram program = new IntProgram(Input);
            program.Change(1, 12);
            program.Change(2, 2);

            program.Run();

            return program.GetResult();
        }

        private static int Part2()
        {
            return 0;
        }

        public class IntProgram
        {
            List<int> Data = new List<int>();
            int Count;

            public IntProgram(List<int> input)
            {
                Data = input;
                Count = Data.Count();
            }

            public void Run()
            {
                bool stop = false;

                for (int i = 0; i < Count; i += 4)
                {
                    int opcode = Data[i];
                    int firstPos = Data[i + 1];
                    int secondPos = Data[i + 2];
                    int resultPos = Data[i + 3];

                    switch (opcode)
                    {
                        case 1:
                            Data[resultPos] = Data[firstPos] + Data[secondPos];
                            break;
                        case 2:
                            Data[resultPos] = Data[firstPos] * Data[secondPos];
                            break;
                        case 99:
                            stop = true;
                            break;
                    }

                    if (stop)
                        break;
                }
            }

            public void Change(int position, int value)
            {
                Data[position] = value;
            }

            public int GetResult() => Data[0];
        }
    }
}
