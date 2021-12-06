using AdventOfCode.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015;

public class Day2 : ISolution
{
    private static List<string> Input =>
        InputHelper.GetInput(2015, 2);

    private static List<Prism> Presents = Input.Select(s => new Prism(s)).ToList();

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
        var total = Presents.Sum(p => p.TotalPaper);

        return total;
    }

    private static int Part2()
    {
        var total = Presents.Sum(p => p.Ribbon);

        return total;
    }

}

public class Prism
{
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int SquareFeet { get; set; }
    public int Slack { get; set; }
    public int TotalPaper { get; set; }
    public int Ribbon { get; set; }

    public Prism(string input)
    {
        var split = input.Split('x');

        Length = int.Parse(split[0]);
        Width = int.Parse(split[1]);
        Height = int.Parse(split[2]);

        int lw = Length * Width;
        int wh = Width * Height;
        int hl = Height * Length;

        Slack = new int[] { lw, wh, hl }.Min();
        SquareFeet = 2 * lw + 2 * wh + 2 * hl;
        TotalPaper = Slack + SquareFeet;

        List<int> dimensions = new() { Length, Width, Height };
        dimensions.Sort();
        int firstLowest = dimensions[0];
        int secondLowest = dimensions[1];

        Ribbon = firstLowest + firstLowest + secondLowest + secondLowest + (Length * Width * Height);
    }
}
