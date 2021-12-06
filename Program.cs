﻿using AdventOfCode._2015;
using AdventOfCode._2019;
using AdventOfCode._2020;
using AdventOfCode._2021;
using System;
using System.Linq;
using System.IO;
using AdventOfCode.Helpers;

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

            string[] yearDirectories = Directory.GetDirectories("..\\..\\..\\Years");
            int[] years = yearDirectories.Select(path => int.Parse(new DirectoryInfo(path).Name)).ToArray();

            if (int.TryParse(input, out int year) && Array.IndexOf(years, year) != -1)
                return year;

            if (string.IsNullOrEmpty(input))
                return DateTime.Now.Year;

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
                Console.WriteLine("We have no solution for this day or you didnt enter a correct number");
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
