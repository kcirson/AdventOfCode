using AdventOfCode._2015;
using AdventOfCode._2019;
using AdventOfCode._2020;
using AdventOfCode._2021;
using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Welcome to the Advent of Code Program!");

            int year = ReadYear("Choose which year you want and we will check if we have solutions for this year (Press enter for current year)");

            StartDayFromYear(year, "Now please choose a day (1-25) to pick a solution for this year");

            Console.ReadLine();
        }

        private static int ReadYear(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);

                string input = Console.ReadLine();

                if (int.TryParse(input, out int year) && Array.IndexOf(Constants.AvailableYears, year) != -1)
                {
                    return year;
                }
                else if (string.IsNullOrEmpty(input))
                {
                    return DateTime.Now.Year;
                }
                else
                {
                    Console.WriteLine("We have no solution for this year or you didnt enter a number");
                }
            }
        }

        private static void StartDayFromYear(int year, string prompt)
        {
            bool stop = false;

            while (!stop)
            {
                Console.WriteLine(prompt);

                if (int.TryParse(Console.ReadLine(), out int day) && day > 0 && day <= 25)
                    stop = StartSolution(year, day);

                if (stop == false)
                    Console.WriteLine("We have no solution for this day or you didnt enter a correct number");
            }
        }

        private static bool StartSolution(int year, int day)
        {
            switch (year)
            {
                case 2015:
                    return Year2015.StartDay(day);
                case 2019:
                    return Year2019.StartDay(day);
                case 2020:
                    return Year2020.StartDay(day);
                case 2021:
                    return Year2021.StartDay(day);
                default:
                    return false;
            }
        }
    }
}
