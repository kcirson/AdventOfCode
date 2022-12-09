namespace AdventOfCode._2015;

public class Day1 : ISolution
{
    private static string Input =>
        InputHelper.GetInputString(2015, 1);

    public void Run()
    {
        Console.WriteLine("Part 1:");
        Console.WriteLine(Part1());
        Console.WriteLine();
        Console.WriteLine("Part 2:");
        Console.WriteLine(Part2(Input));
    }

    private static int Part1()
    {
        return GetFloor(Input);
    }

    private static int GetFloor(string input)
    {
        int floor = 0;
        foreach (char c in input)
        {
            if (c == '(')
                floor++;

            if (c == ')')
                floor--;
        }

        return floor;
    }

    private static int Part2(string input)
    {
        int floor = 0;
        int length = input.Length;

        for (int i = 0; i < length; i++)
        {
            char c = input[i];

            if (c == '(')
                floor++;
            else if (c == ')')
                floor--;

            if (floor == -1)
                return i + 1;
        }

        return -1;
    }
}
