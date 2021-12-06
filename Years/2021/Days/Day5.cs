using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021;
public class Day5 : ISolution
{
    private static List<string> Input =>
        InputHelper.GetInput(2021, 5);

    //private static List<string> Input =>
    //    new()
    //    {
    //        "0,9 -> 5,9",
    //        "8,0 -> 0,8",
    //        "9,4 -> 3,4",
    //        "2,2 -> 2,1",
    //        "7,0 -> 7,4",
    //        "6,4 -> 2,0",
    //        "0,9 -> 2,9",
    //        "3,4 -> 1,4",
    //        "0,0 -> 8,8",
    //        "5,5 -> 8,2"
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
        Map map = new();

        foreach (string line in Input)
            map.AddLineNoDiagonal(line);

        return map.GetPointsWithOverlaps();
    }

    private static int Part2()
    {
        Map map = new();

        foreach (string line in Input)
            map.AddLine(line);

        return map.GetPointsWithOverlaps();
    }
}

public class Map
{
    Dictionary<string, int> Points { get; set; }

    public Map()
    {
        Points = new();
    }

    public void AddLineNoDiagonal(string line)
    {
        string[] entries = line.Split("->", StringSplitOptions.None);
        string from = entries[0];
        string to = entries[1];

        string[] fromSplit = from.Split(',');
        string[] toSplit = to.Split(',');

        _ = int.TryParse(fromSplit[0], out int fromFirst);
        _ = int.TryParse(fromSplit[1], out int fromSecond);
        _ = int.TryParse(toSplit[0], out int toFirst);
        _ = int.TryParse(toSplit[1], out int toSecond);

        if (!(fromFirst == toFirst || fromSecond == toSecond))
            return;

        if (fromFirst == toFirst)
        {
            if (fromSecond < toSecond)
            {
                for (int i = fromSecond; i <= toSecond; i++)
                    AddPoint(fromFirst, i);
            }
            else
            {
                for (int i = fromSecond; i >= toSecond; i--)
                    AddPoint(toFirst, i);
            }

            return;
        }

        if (fromSecond == toSecond)
        {
            if (fromFirst < toFirst)
            {
                for (int i = fromFirst; i <= toFirst; i++)
                    AddPoint(i, fromSecond);
            }
            else
            {
                for (int i = fromFirst; i >= toFirst; i--)
                    AddPoint(i, fromSecond);
            }

            return;
        }
    }

    public void AddLine(string line)
    {
        string[] entries = line.Split("->", StringSplitOptions.None);
        string from = entries[0];
        string to = entries[1];

        string[] fromSplit = from.Split(',');
        string[] toSplit = to.Split(',');

        _ = int.TryParse(fromSplit[0], out int fromFirst);
        _ = int.TryParse(fromSplit[1], out int fromSecond);
        _ = int.TryParse(toSplit[0], out int toFirst);
        _ = int.TryParse(toSplit[1], out int toSecond);

        if (fromFirst == toFirst)
        {
            if (fromSecond < toSecond)
            {
                for (int i = fromSecond; i <= toSecond; i++)
                    AddPoint(fromFirst, i);
            }
            else
            {
                for (int i = fromSecond; i >= toSecond; i--)
                    AddPoint(toFirst, i);
            }

            return;
        }

        if (fromSecond == toSecond)
        {
            if (fromFirst < toFirst)
            {
                for (int i = fromFirst; i <= toFirst; i++)
                    AddPoint(i, fromSecond);
            }
            else
            {
                for (int i = fromFirst; i >= toFirst; i--)
                    AddPoint(i, fromSecond);
            }

            return;
        }

        if (fromFirst < toFirst)
        {
            if (fromSecond < toSecond)
            {
                while (fromFirst != toFirst && fromSecond != toSecond)
                {
                    AddPoint(fromFirst, fromSecond);
                    fromFirst++;
                    fromSecond++;
                }
                AddPoint(fromFirst, fromSecond);
            }
            else
            {
                while (fromFirst != toFirst && fromSecond != toSecond)
                {
                    AddPoint(fromFirst, fromSecond);
                    fromFirst++;
                    fromSecond--;
                }
                AddPoint(fromFirst, fromSecond);
            }
        }
        else
        {
            if (fromSecond < toSecond)
            {
                while(fromFirst != toFirst && fromSecond != toSecond)
                {
                    AddPoint(fromFirst, fromSecond);
                    fromFirst--;
                    fromSecond++;
                }
                AddPoint(fromFirst, fromSecond);
            }
            else
            {
                while (fromFirst != toFirst && fromSecond != toSecond)
                {
                    AddPoint(fromFirst, fromSecond);
                    fromFirst--;
                    fromSecond--;
                }
                AddPoint(fromFirst, fromSecond);
            }
        }
    }

    public int GetPointsWithOverlaps(int OverlapCount = 2)
    {
        return Points.Count(p => p.Value >= OverlapCount);
    }

    private void AddPoint(int i, int j)
    {
        string key = $"{i},{j}";
        if (Points.ContainsKey(key))
            Points[key] = Points[key] + 1;
        else
            Points.Add(key, 1);
    }
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public int OverlapCount { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}
