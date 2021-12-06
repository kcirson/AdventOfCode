using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015;

public class Day6 : ISolution
{
    private static List<string> Input =>
    InputHelper.GetInput(2015, 6);

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
        HomeDeco deco = new();

        foreach (string command in Input)
        {
            deco.ExecuteCommandPart1(command);
        }

        return deco.GetLitLights();
    }

    private static int Part2()
    {
        HomeDeco deco = new();

        foreach (string command in Input)
        {
            deco.ExecuteCommandPart2(command);
        }

        return deco.GetTotalBrightness();
    }
}

public class HomeDeco
{
    public int[,] LightGrid { get; set; }

    public HomeDeco()
    {
        LightGrid = new int[1000, 1000];
    }

    public void ExecuteCommandPart1(string command)
    {
        string[] split = command.Split(' ');
        string firstCoords = (split[0] == "turn") ? split[2] : split[1]; ;
        string secondCoords = split.Last();

        string[] splitFirst = firstCoords.Split(',');
        string[] splitSecond = secondCoords.Split(',');

        if (int.TryParse(splitFirst[0], out int startX) &&
            int.TryParse(splitFirst[1], out int startY) &&
            int.TryParse(splitSecond[0], out int endX) &&
            int.TryParse(splitSecond[1], out int endY)
            )
        {
            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    if (split[0] == "toggle")
                    {
                        LightGrid[i, j] = LightGrid[i, j] == 1 ? 0 : 1;
                        continue;
                    }

                    LightGrid[i, j] = split[1] == "on" ? 1 : 0;
                }
            }
        }
        else
        {
            throw new Exception("something went wrong");
        }
    }

    public void ExecuteCommandPart2(string command)
    {
        string[] split = command.Split(' ');
        string firstCoords = (split[0] == "turn") ? split[2] : split[1]; ;
        string secondCoords = split.Last();

        string[] splitFirst = firstCoords.Split(',');
        string[] splitSecond = secondCoords.Split(',');

        if (int.TryParse(splitFirst[0], out int startX) &&
            int.TryParse(splitFirst[1], out int startY) &&
            int.TryParse(splitSecond[0], out int endX) &&
            int.TryParse(splitSecond[1], out int endY)
            )
        {
            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    if (split[0] == "toggle")
                    {
                        LightGrid[i, j] = LightGrid[i, j] + 2;
                        continue;
                    }

                    if (split[1] == "on")
                    {
                        LightGrid[i, j] = LightGrid[i, j] + 1;
                        continue;
                    }

                    if (LightGrid[i, j] > 0)
                    {
                        LightGrid[i, j] = LightGrid[i, j] - 1;
                    }
                }
            }
        }
        else
        {
            throw new Exception("something went wrong");
        }
    }

    public int GetLitLights()
    {
        int count = 0;

        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                if (LightGrid[i, j] == 1)
                    count++;
            }
        }

        return count;
    }

    public int GetTotalBrightness()
    {
        int brightness = 0;

        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                brightness += LightGrid[i, j];
            }
        }

        return brightness;
    }
}
