namespace AdventOfCode._2021;

public class Day1 : ISolution
{
    private static List<int> Input =>
        InputHelper.GetInput(2021, 1).Select(int.Parse).ToList();

    public void Run()
    {
        Console.WriteLine("Part 1:");
        Console.WriteLine(Part1());
        Console.WriteLine();
        Console.WriteLine("Part 2:");
        Console.WriteLine(Part2());
    }

    private int Part1()
    {
        int prev = 0;
        int increased = 0;

        foreach (int value in Input)
        {
            if (prev == 0)
            {
                prev = value;
                continue;
            }

            if (prev < value)
                increased++;

            prev = value;
        }

        return increased;
    }

    private int Part2()
    {
        int count = Input.Count;
        int prev = 0;
        int increased = 0;

        for (int i = 0; i < count; i++)
        {
            int first = Input[i];
            int second = 0;
            int third = 0;

            if (i + 1 < count)
                second = Input[i + 1];

            if (i + 2 < count)
                third = Input[i + 2];

            int sum = first + second + third;

            if (prev == 0)
            {
                prev = sum;
                continue;
            }

            if (prev < sum)
                increased++;

            prev = sum;
        }

        return increased;
    }
}
