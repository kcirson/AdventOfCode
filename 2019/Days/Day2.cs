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
            IntProgram program = new IntProgram(Input, 12 ,2);
            program.Run();

            return program.GetResult();
        }

        private static int Part2()
        {
            IntProgram program;
            int count = Input.Count;
            int target = 19690720;
            int noun = 0, verb = 0;

            for(int i = 0; i < count; i++)
            {
                for(int j = 0; j < count; j++)
                {
                    program = new IntProgram(Input, i, j);
                    program.Run();

                    if (program.GetResult() == target)
                    {
                        noun = i;
                        verb = j;
                        break;
                    }
                }

                if (noun != 0 && verb != 0)
                    break;
            }

            return 100 * noun + verb;
        }

        public class IntProgram
        {
            List<int> Data = new List<int>();
            int Count;

            public IntProgram(List<int> input, int noun, int verb)
            {
                Data = input;
                Count = Data.Count;

                Data[1] = noun;
                Data[2] = verb;
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

            public int GetResult() => Data[0];
        }
    }
}
