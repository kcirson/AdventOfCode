namespace AdventOfCode._2021;

public class Year2021
{
    public static bool StartDay(int day)
    {
        switch (day)
        {
            case 1:
                Day1.Run();
                return true;
            case 2:
                Day2.Run();
                return true;
            default:
                return false;
        }
    }
}
