using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020;

public static class Day9
{
    private static List<string> Input =>
        InputHelper.GetInput(2020, 9);

    public static void Run()
    {
        Console.WriteLine("Part 1:");
        Console.WriteLine(Part1());
        Console.WriteLine();
        Console.WriteLine("Part 2:");
        Console.WriteLine(Part2());
    }

    private static long Part1()
    {
        List<long> numbers = Input.Select(long.Parse).ToList();
        int count = numbers.Count;
        Queue<long> preamble = new();
        bool stop = false;
        long noSum = 0;

        for (int i = 0; i < 25; i++)
            preamble.Enqueue(numbers[i]);

        for (int i = 25; i < count; i++)
        {
            long number = numbers[i];

            foreach (long num in preamble)
            {
                long numberToFind = number - num;

                if (preamble.Contains(numberToFind) && num != numberToFind)
                {
                    break;
                }

                if (num == preamble.Last())
                    stop = true;
            }

            if (stop)
            {
                noSum = number;
                break;
            }
            else
            {

            }

            preamble.Dequeue();
            preamble.Enqueue(number);
        }

        return noSum;
    }

    private static long Part2()
    {
        List<long> numbers = Input.Select(long.Parse).ToList();
        long number = Part1();
        int count = numbers.Count; //numbers.IndexOf(number);
        int numberIndex = numbers.IndexOf(number);
        List<long> set = new();

        long sum = 0;

        Queue<long> numberQueue = new(numbers.GetRange(0, numberIndex));
        bool stop = false;

        while (!stop)
        {
            for (int i = 0; i < numberIndex; i++)
            {
                long current = numberQueue.ToList()[i];
                long added = sum + current;

                if (added == number)
                {
                    stop = true;
                    set = numberQueue.ToList().GetRange(0, i);

                    return set.Min() + set.Max();
                }
                else
                {
                    sum = added;
                }
            }

            if (!stop)
                numberQueue.Dequeue();

            numberIndex = numberQueue.Count;
            sum = 0;
        }

        return 0;
    }
}
