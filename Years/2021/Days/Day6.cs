using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021;
public class Day6 : ISolution
{
    private static List<int> Input =>
         InputHelper.GetInputString(2021, 6).Split(',').Select(int.Parse).ToList();

    //private static List<int> Input =>
    //    new()
    //    {
    //        3,
    //        4,
    //        3,
    //        1,
    //        2
    //    };

    public void Run()
    {
        Console.WriteLine("Part 1:");
        Console.WriteLine(Part1());
        Console.WriteLine();
        Console.WriteLine("Part 2:");
        Console.WriteLine(Part2());
    }

    private static long Part1()
    {
        return MakeFish(80);
    }

    private static long Part2()
    {
        return MakeFish(256);
    }

    private static long MakeFish(int days)
    {
        Dictionary<int, long> fishes = new();

        foreach (int timer in Input)
        {
            if (fishes.ContainsKey(timer))
            {
                fishes[timer]++;
            }
            else
            {
                fishes.Add(timer, 1);
            }
        }

        for (int i = 0; i < days; i++)
        {
            Dictionary<int, long> next = new();

            foreach (int key in fishes.Keys)
            {
                if (key == 0)
                {
                    if (next.ContainsKey(8))
                        next[8] = fishes[key];
                    else
                        next.Add(8, fishes[key]);

                    if (next.ContainsKey(6))
                        next[6] += fishes[key];
                    else
                        next.Add(6, fishes[key]);
                }
                else
                {
                    int newkey = key - 1;

                    if (next.ContainsKey(newkey))
                    {
                        if (newkey == 6)
                            next[newkey] += fishes[key];
                        else
                            next[newkey] = fishes[key];
                    }
                    else
                        next.Add(newkey, fishes[key]);
                }
            }

            fishes = next;
        }

        return fishes.Sum(f => f.Value);
    }
}
