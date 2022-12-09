namespace AdventOfCode._2019;

public class Day1 : ISolution
{
    private static List<int> Input =>
        InputHelper.GetInput(2019, 1).Select(int.Parse).ToList();

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
        return Input.Select(i => CalculateFuel1(i)).Sum();
    }

    private static int Part2()
    {
        return Input.Select(i => CalculateFuel2(i)).Sum();
    }

    private static int CalculateFuel1(int mass)
    {
        return Convert.ToInt32(Math.Floor(mass / 3.0) - 2);
    }

    private static int CalculateFuel2(int mass)
    {
        int massFuel = mass;
        int totalFuel = 0;

        while (massFuel > 0)
        {
            massFuel = CalculateFuel1(massFuel);

            if (massFuel > 0)
                totalFuel += massFuel;
        }

        return totalFuel;
    }
}
