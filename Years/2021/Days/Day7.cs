using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021;
public class Day7 : ISolution
{
    private static List<int> Input =>
         InputHelper.GetInputString(2021, 7).Split(',').Select(int.Parse).ToList();

    //public static List<int> Input =>
    //    new()
    //    {
    //        16,
    //        1,
    //        2,
    //        0,
    //        4,
    //        2,
    //        7,
    //        1,
    //        2,
    //        14
    //    };

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
        Dictionary<int, int> crabs = Input.GroupBy(i => i).ToDictionary(g => g.Key, g => g.Count());
        Dictionary<int, int> locations = new();

        int min = crabs.Keys.Min();
        int max = crabs.Keys.Max();
        int lowestFuel = 0;

        for (int i = min; i < max; i++)
        {
            int goTo = i;
            int currentFuel = 0;

            foreach (int crab in crabs.Keys)
            {
                int amountOfCrabs = crabs[crab];
                int fuel = crab - goTo;

                fuel = Math.Abs(fuel);

                currentFuel += fuel * amountOfCrabs;
            }
            
            locations.Add(goTo, currentFuel);

            if (i == min || currentFuel < lowestFuel)
                lowestFuel = currentFuel;

        }

        return lowestFuel;
    }

    private static int Part2()
    {
        Dictionary<int, int> crabs = Input.GroupBy(i => i).ToDictionary(g => g.Key, g => g.Count());
        Dictionary<int, int> locations = new();

        int min = crabs.Keys.Min();
        int max = crabs.Keys.Max();
        int lowestFuel = 0;

        for (int i = min; i < max; i++)
        {
            int goTo = i;
            int currentFuel = 0;

            foreach (int crab in crabs.Keys)
            {
                int moveTo = Math.Abs(crab - goTo);
                int fuel = 0;

                for (int j = 0; j <= moveTo; j++)
                {
                    fuel += j;
                }

                int amountOfCrabs = crabs[crab];

                fuel = Math.Abs(fuel);

                currentFuel += fuel * amountOfCrabs;
            }

            locations.Add(goTo, currentFuel);

            if (i == min || currentFuel < lowestFuel)
                lowestFuel = currentFuel;

        }

        return lowestFuel;
    }
}
