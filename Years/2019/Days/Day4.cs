using AdventOfCode.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2019;

public class Day4 : ISolution
{
    private static string Input =>
        InputHelper.GetInputString(2019, 4);

    public void Run()
    {
        Console.WriteLine("Part 1:");
        Console.WriteLine(Part1());
        Console.WriteLine();
        Console.WriteLine("Part 2:");
        Console.WriteLine(Part2());
    }

    public static int Part1()
    {
        List<int> CorrectPasswords = new();

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
        List<int> CorrectPasswords = new();
        List<int> AdvancedPasswords = new();

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

        List<int> numbersTwice = new();

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

    
}

