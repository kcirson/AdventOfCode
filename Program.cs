using AdventOfCode._2015;
using AdventOfCode._2019;
using AdventOfCode._2020;
using AdventOfCode._2021;
using System;
using System.Linq;
using System.IO;
using AdventOfCode.Helpers;
using System.Collections.Generic;

namespace AdventOfCode;

class Program
{
    static void Main()
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
            if (string.IsNullOrEmpty(input))
                input = DateTime.Now.Year.ToString();

            if (int.TryParse(input, out int year))
            {
                string[] yearDirectories = Directory.GetDirectories("..\\..\\..\\Years");
                int[] years = yearDirectories.Select(path => int.Parse(new DirectoryInfo(path).Name)).ToArray();

                if (Array.IndexOf(years, year) != -1)
                    return year;
            }

            Console.WriteLine("We have no solution for this year, do you want to create it? Y/N");

            string choice = Console.ReadLine().ToLower();

            if (choice == "y")
            {
                Directory.CreateDirectory($"..\\..\\..\\Years\\{year}");
                Directory.CreateDirectory($"..\\..\\..\\Years\\{year}\\Days");
                Directory.CreateDirectory($"..\\..\\..\\Years\\{year}\\Inputs");

                return year;
            }

            Console.WriteLine("We have no solution for this year");
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
            {
                Console.WriteLine($"We have no solution for this day, do you want to create one for {day}? Y/N");

                string choice = Console.ReadLine().ToLower();

                if (choice == "y")
                {
                    DayCreator.CreateDay(year, day);
                    
                    Console.WriteLine($"Good luck with day {day}!");
                }
                else
                {
                    Console.WriteLine("We have no solution for this year");
                }

            }
        }
    }

    private static bool StartSolution(int year, int day)
    {
        ISolution solution = GetInstance($"AdventOfCode._{year}.Day{day}");

        if (solution != null)
        {
            solution.Run();
            return true;
        }

        return false;
    }

    private static ISolution GetInstance(string strFullyQualifiedName)
    {
        Type t = Type.GetType(strFullyQualifiedName);
        try
        {
            return (ISolution)Activator.CreateInstance(t);
        }
        catch
        {
            return null;
        }
    }
}
