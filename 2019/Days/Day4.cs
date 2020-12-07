using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class Day4
    {
        private static string Input =>
            InputHelper.GetInputString(2019, 4);

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
            List<int> foundNumber = new List<int>();
            string[] numbers = Input.Split('-');

            if (int.TryParse(numbers[0], out int firstNumber))
            {
                if (int.TryParse(numbers[1], out int secondNumber))
                {
                    for (int i = firstNumber; i <= secondNumber; i++)
                    {
                        bool doubleNumbers = false;
                        bool increasingNumbers = false;
                        char[] chars = $"{i}".ToCharArray();

                        int prevNumbers = -1;

                        foreach (char c in chars)
                        {
                            if (int.TryParse($"{c}", out int number))
                            {
                                if (prevNumbers == -1)
                                {
                                    prevNumbers = number;
                                    continue;
                                }

                                if (!doubleNumbers)
                                    doubleNumbers = number == prevNumbers;

                                if (number >= prevNumbers)
                                {
                                    prevNumbers = number;
                                    increasingNumbers = true;
                                }
                                else
                                {
                                    increasingNumbers = false;
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (doubleNumbers && increasingNumbers)
                            foundNumber.Add(i);
                    }
                }
            }

            return foundNumber.Count;
        }

        private static int Part2()
        {
            return 0;
        }
    }
}
