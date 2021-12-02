using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021;

public static class Day2
{
    private static List<string> Input =>
        InputHelper.GetInput(2021, 2);

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
        List<WorldMap> map = new() { new WorldMap(0, 0) };

        foreach (string action in Input)
        {
            string[] split = action.Split(' ');

            if (int.TryParse(split[1], out int steps))
            {
                WorldMap lastPosition = map.Last();

                switch (split[0])
                {
                    case "forward":
                        map.Add(new WorldMap(lastPosition.HorizontalPoistion + steps, lastPosition.Depth));
                        break;
                    case "down":
                        map.Add(new WorldMap(lastPosition.HorizontalPoistion, lastPosition.Depth + steps));
                        break;
                    case "up":
                        map.Add(new WorldMap(lastPosition.HorizontalPoistion, lastPosition.Depth - steps));
                        break;
                }
            }
        }

        WorldMap finalPosition = map.Last();

        return finalPosition.Depth * finalPosition.HorizontalPoistion;
    }

    private static int Part2()
    {
        List<WorldMap> map = new() { new WorldMap(0, 0, 0) };

        foreach (string action in Input)
        {
            string[] split = action.Split(' ');

            if (int.TryParse(split[1], out int steps))
            {
                WorldMap lastPosition = map.Last();

                switch (split[0])
                {
                    case "forward":
                        map.Add(new WorldMap(lastPosition.HorizontalPoistion + steps, lastPosition.Depth + lastPosition.Aim * steps, lastPosition.Aim));
                        break;
                    case "down":
                        map.Add(new WorldMap(lastPosition.HorizontalPoistion, lastPosition.Depth, lastPosition.Aim + steps));
                        break;
                    case "up":
                        map.Add(new WorldMap(lastPosition.HorizontalPoistion, lastPosition.Depth, lastPosition.Aim - steps));
                        break;
                }
            }
        }

        WorldMap finalPosition = map.Last();

        return finalPosition.Depth * finalPosition.HorizontalPoistion;
    }
}

public class WorldMap
{
    public int HorizontalPoistion { get; set; }
    public int Depth { get; set; }
    public int Aim { get; set; }

    public WorldMap(int hpos, int depth, int aim = 0)
    {
        HorizontalPoistion = hpos;
        Depth = depth;
        Aim = aim;
    }
}
