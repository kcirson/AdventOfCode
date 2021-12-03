﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021;

public class Day3
{
    private static List<string> Input =>
    InputHelper.GetInput(2021, 3);

    //private static List<string> Input = new List<string> {
    //    "00100",
    //    "11110",
    //    "10110",
    //    "10111",
    //    "10101",
    //    "01111",
    //    "00111",
    //    "11100",
    //    "10000",
    //    "11001",
    //    "00010",
    //    "01010"};

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
        string gammaString = string.Empty;
        string epsilonString = string.Empty;

        int length = Input.First().Length;

        for (int i = 0; i < length; i++)
        {
            int amountOfOnes = Input.Where(val => val[i] == '1').Count();
            int amountOfZeroes = Input.Where(val => val[i] == '0').Count();

            if (amountOfOnes > amountOfZeroes)
            {
                gammaString += "1";
                epsilonString += "0";
            }
            else
            {
                gammaString += "0";
                epsilonString += "1";
            }
        }

        int gamma = Convert.ToInt32(gammaString, 2);
        int epsilon = Convert.ToInt32(epsilonString, 2);

        return gamma * epsilon;
    }

    private static int Part2()
    {
        List<string> oxygen = new(Input);
        List<string> scrubber = new(Input);

        int length = oxygen.First().Length;

        for (int i = 0; i < length + 1; i++)
        {
            if (oxygen.Count == 1 && scrubber.Count == 1)
                break;

            if (oxygen.Count > 1)
            {
                int amountOfOnes = oxygen.Where(val => val[i] == '1').Count();
                int amountOfZeroes = oxygen.Where(val => val[i] == '0').Count();

                if (amountOfOnes == amountOfZeroes || amountOfOnes > amountOfZeroes)
                {
                    oxygen = oxygen.Where(val => val[i] == '1').ToList();
                }
                else
                {
                    oxygen = oxygen.Where(val => val[i] == '0').ToList();
                }
            }

            if (scrubber.Count > 1)
            {
                int amountOfOnes = scrubber.Where(val => val[i] == '1').Count();
                int amountOfZeroes = scrubber.Where(val => val[i] == '0').Count();

                if (amountOfOnes == amountOfZeroes || amountOfOnes > amountOfZeroes)
                {
                    scrubber = scrubber.Where(val => val[i] == '0').ToList();
                }
                else
                {
                    scrubber = scrubber.Where(val => val[i] == '1').ToList();
                }
            }
        }

        int oxygenData = Convert.ToInt32(oxygen.First(), 2);
        int scrubberData = Convert.ToInt32(scrubber.First(), 2);

        return oxygenData * scrubberData;
    }
}
