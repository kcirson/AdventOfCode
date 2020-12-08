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
            Game game = new Game();
            Queue<string> jumps = new Queue<string>(Input.FindAll(s => s.Contains("jmp")));
            Queue<string> nops = new Queue<string>(Input.FindAll(s => s.Contains("nop")));

            bool ExitedNormally = false;

            while (!ExitedNormally)
            {
                game.Reset();
                var temp = Input;

                if(jumps.Count > 0)
                {
                    string jump = jumps.Dequeue();
                    string operation = temp.First(op => op == jump);
                    temp[temp.IndexOf(operation)] = operation.Replace("jmp", "nop");
                }
                else if(nops.Count > 0)
                {
                    string nop = nops.Dequeue();
                    string operation = temp.First(op => op == nop);

                    temp[temp.IndexOf(operation)] = operation.Replace("nop", "jmp");
                }
                else
                {
                    ExitedNormally = true;
                }

                game.Run(temp);

                ExitedNormally = game.HasExitedNormally();
            }

            return game.GetAccumulator();
        }

        private class Game
        {
            private int Accumulator { get; set; }
            private Dictionary<int, string> Instructions { get; set; }
            private int AmountOfInstruction { get; set; }
            private HashSet<int> PastInstructions { get; set; }

            public Game()
            {
                Reset();
            }

            public void Run(List<string> values)
            {
                AmountOfInstruction = values.Count;
                for (int i = 0; i < AmountOfInstruction; i++)
                {
                    Instructions.Add(i, values[i]);
                }

                bool stop = false;
                int action = 0;

                while (!stop)
                {
                    if(action < Instructions.Count)
                    {
                        string value = Instructions[action];
                        stop = DoAction(value, action, out action);
                    }
                    else
                    {
                        stop = true;
                    }
                }
            }

            public void Reset()
            {
                Accumulator = 0;
                PastInstructions = new HashSet<int>();
                Instructions = new Dictionary<int, string>();
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
            public bool HasExitedNormally() => PastInstructions.Last() == Instructions.Last().Key;

        }
    }
}
