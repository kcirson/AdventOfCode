using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day8
    {
        private static List<string> Input =>
            InputHelper.GetInput(2020, 8);

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
            Game game = new Game();
            game.Run(Input);

            return game.GetAccumulator();
        }

        private static int Part2()
        {
            return 0;
        }

        private class Game
        {
            private int Accumulator { get; set; }
            Dictionary<int, string> Instructions { get; set; }
            List<int> PastInstructions { get; set; }

            public Game()
            {
                Accumulator = 0;
                PastInstructions = new List<int>();
                Instructions = new Dictionary<int, string>();
            }

            public void Run(List<string> values)
            {
                int count = values.Count;
                for (int i = 0; i < count; i++)
                {
                    Instructions.Add(i, values[i]);
                }

                bool stop = false;
                int action = 0;

                while (!stop)
                {
                    string value = Instructions[action];
                    stop = DoAction(value, action, out action);
                }
            }


            private bool DoAction(string operation, int current, out int next)
            {
                bool stop = false;
                next = 0;

                if (PastInstructions.Contains(current))
                {
                    stop = true;
                    return stop;
                }

                PastInstructions.Add(current);

                string[] split = operation.Split(' ');

                if (int.TryParse(split[1], out int increase))
                {
                    switch (split[0])
                    {
                        case "nop":
                            next = current + 1;
                            break;
                        case "acc":
                            Accumulator += increase;
                            next = current + 1;
                            break;
                        case "jmp":
                            next = current + increase;
                            break;
                    }
                }

                return stop;
            }

            public int GetAccumulator() => Accumulator;

        }
    }
}
