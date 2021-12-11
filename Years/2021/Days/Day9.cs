using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021;
public class Day9 : ISolution
{
    private static List<string> Input =>
         InputHelper.GetInput(2021, 9).ToList();

    //private static List<string> Input =>
    //    new()
    //    {
    //        "2199943210",
    //        "3987894921",
    //        "9856789892",
    //        "8767896789",
    //        "9899965678"
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
        return FindLowPoints();
    }

    private static int Part2()
    {
        return FindBasins();
    }

    private static int FindBasins()
    {
        int count = Input.Count;
        int[][] map = new int[count][];

        List<int> basinSize = new();

        for (int i = 0; i < count; i++)
        {
            map[i] = Input[i].Select(x => Convert.ToInt32(x.ToString())).ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            int length = map[i].Length;

            for (int j = 0; j < length; j++)
            {
                if (IsLowerThenAdjecent(i, j, map))
                {
                    basinSize.Add(CheckBasinSize(i, j, map));
                }
            }
        }

        return basinSize.OrderByDescending(x => x).Take(3).Aggregate((x, y) => x * y);
    }

    private static int CheckBasinSize(int row, int position, int[][] map)
    {
        int current = map[row][position];
        int rowLength = map[row].Length;
        int mapSize = map.Length;

        Queue<(int x, int y)> toCheck = new();
        toCheck.Enqueue((row, position));

        HashSet<(int x, int y)> basin = new();

        while(toCheck.Count > 0)
        {
            var coord = toCheck.Dequeue();
            if (!basin.Contains(coord))
            {
                basin.Add(coord);
                var value = map[coord.x][coord.y];

                if(value == 9)
                    continue;

                int left = coord.y - 1;
                int right = coord.y + 1;
                int up = coord.x - 1;
                int down = coord.x + 1;

                if (left >= 0 && left < rowLength)
                    toCheck.Enqueue((coord.x, left));

                if (right >= 0 && right < rowLength)
                    toCheck.Enqueue((coord.x, right));

                if (up >= 0 && up < mapSize)
                    toCheck.Enqueue((up, coord.y));

                if (down >= 0 && down < mapSize)
                    toCheck.Enqueue((down, coord.y));
            }
        }

        return basin.Count(x => map[x.x][x.y] != 9);
    }

    private static int FindLowPoints()
    {
        int count = Input.Count;
        int[][] map = new int[count][];

        List<int> lowestPoint = new();

        for (int i = 0; i < count; i++)
        {
            map[i] = Input[i].Select(x => Convert.ToInt32(x.ToString())).ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            int length = map[i].Length;

            for (int j = 0; j < length; j++)
            {
                if (IsLowerThenAdjecent(i, j, map))
                {
                    lowestPoint.Add(map[i][j] + 1);
                }
            }
        }

        return lowestPoint.Sum();
    }

    private static bool IsLowerThenAdjecent(int row, int position, int[][] map)
    {
        int current = map[row][position];
        int left = position - 1;
        int right = position + 1;
        int up = row - 1;
        int down = row + 1;

        if (row == 0)
        {
            if (position == 0)
            {
                return (current < map[row][right] &&
                        current < map[down][position]);
            }
            else
            {
                if (right < map[row].Length)
                {
                    return (current < map[row][right] &&
                            current < map[row][left] &&
                            current < map[down][position]);
                }
                else
                {
                    return (current < map[row][left] &&
                            current < map[down][position]);
                }

            }
        }

        if (row == map.Length - 1)
        {
            if (position == 0)
            {
                return (current < map[row][right] &&
                        current < map[up][position]);
            }
            else
            {
                if (right < map[row].Length)
                {
                    return (current < map[row][right] &&
                        current < map[row][left] &&
                        current < map[up][position]);
                }

                return (current < map[row][left] &&
                        current < map[up][position]);
            }
        }

        if (position == 0)
        {
            return (current < map[row][right] &&
                    current < map[up][position] &&
                    current < map[down][position]);
        }

        if (right < map[row].Length)
        {
            return (current < map[row][right] &&
                    current < map[row][left] &&
                    current < map[up][position] &&
                    current < map[down][position]);
        }
        else
        {
            return (current < map[row][left] &&
                    current < map[up][position] &&
                    current < map[down][position]);

        }
    }

}
