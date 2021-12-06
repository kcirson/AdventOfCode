using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AdventOfCode.Helpers;

namespace AdventOfCode._2020;

public class Day1 : ISolution
{
    private static List<int> Input =>
        InputHelper.GetInput(2020, 1).Select(int.Parse).ToList();

    public void Run()
    {
        Console.WriteLine("Part 1:");
        Console.WriteLine(Part1());
        Console.WriteLine();
        Console.WriteLine("Part 2:");
        Console.WriteLine(Part2());
    }

    private static int Part1()
    {
        int number1 = -1;
        int number2 = -1;

        foreach (int number in Input)
        {
            var numberToFind = 2020 - number;

            if (Input.Find(i => i == numberToFind) is int found && found != 0)
            {
                number1 = number;
                number2 = found;
                break;
            }
        }

        return number1 * number2;
    }

    private static int Part2()
    {
        int number1 = 0;
        int number2 = 0;
        int number3 = 0;

        List<int> values = new();
        int count = Input.Count;

        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                int numberToFind = 2020 - Input[i] - Input[j];

                if (Input.Find(i => i == numberToFind) is int found && found != 0)
                {
                    number1 = Input[i];
                    number2 = Input[j];
                    number3 = found;
                    break;
                }
            }
            if (number3 != 0)
                break;
        }

        return number1 * number2 * number3;
    }
}
