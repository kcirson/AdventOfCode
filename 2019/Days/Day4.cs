using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static int Part1()
        {
            List<int> CorrectPasswords = new List<int>();

            string[] numbers = Input.Split('-');

            if (int.TryParse(numbers[0], out int firstNumber))
            {
                if (int.TryParse(numbers[1], out int secondNumber))
                {
                    for (int i = firstNumber; i <= secondNumber; i++)
                    {
                        if (MeetsCriteria(i))
                            CorrectPasswords.Add(i);
                    }
                }
            }

            return CorrectPasswords.Count;
        }

        public static int Part2()
        {
            List<int> CorrectPasswords = new List<int>();
            List<int> AdvancedPasswords = new List<int>();

            string[] numbers = Input.Split('-');

            if (int.TryParse(numbers[0], out int firstNumber))
            {
                if (int.TryParse(numbers[1], out int secondNumber))
                {
                    for (int i = firstNumber; i <= secondNumber; i++)
                    {
                        if (MeetsCriteria(i))
                            CorrectPasswords.Add(i);
                    }
                }
            }

            foreach (int simpleCriteria in CorrectPasswords)
            {
                if (AdvancedCriteria(simpleCriteria))
                    AdvancedPasswords.Add(simpleCriteria);
            }

            return AdvancedPasswords.Count;
        }

        private static bool MeetsCriteria(int passWord)
        {
            bool hasSameNumber = false;
            bool meets = false;
            List<int> individual = passWord.ToIntList();
            int length = individual.Count;

            for (int i = 0; i < length; i++)
            {
                if (i + 1 < length)
                {
                    if (individual[i] == individual[i + 1])
                        hasSameNumber = true;

                    if (individual[i] <= individual[i + 1])
                        meets = true;
                    else
                    {
                        meets = false;
                        break;
                    }

                }
            }

            return meets && hasSameNumber;
        }

        private static bool AdvancedCriteria(int simpleCriteria)
        {
            List<int> individual = simpleCriteria.ToIntList();
            int length = individual.Count - 1;

            List<int> numbersTwice = new List<int>();

            for (int i = 0; i < length; i++)
            {
                int current = individual[i];

                if (individual.Count(integer => integer == current) == 2)
                {
                    if (!numbersTwice.Contains(i))
                        numbersTwice.Add(i);
                }
            }

            return numbersTwice.Count > 0;
        }

        private static List<int> ToIntList(this int integer)
        {
            List<int> individual = new List<int>();

            while (integer > 0)
            {
                individual.Add(integer % 10);
                integer /= 10;
            }

            individual.Reverse();

            return individual;
        }
    }
}
