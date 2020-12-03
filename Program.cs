using AdventOfCode._2019;
using AdventOfCode._2020;
using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Welcome to the Advent of Code Program!");
            
            int year = ReadYear("Choose which year you want and we will check if we have solutions for this year (Press enter for current year)");
            int day = ReadDay("Now please choose a day (1-25) to pick a solution for this year");

            switch (year)
            {
                case 2019:
                    new Year2019(day);
                    break;
                case 2020:
                    new Year2020(day);
                    break;
            }

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

        private static int ReadDay(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);

                if (int.TryParse(Console.ReadLine(), out int day) && day > 0 && day <= 25)
                {
                    return day;
                }
                else
                {
                    Console.WriteLine("We have no solution for this day or you didnt enter a correct number");
                }
            }
        }
    }
}
