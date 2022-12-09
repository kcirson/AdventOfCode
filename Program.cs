using System.IO;

Console.WriteLine("Hello Welcome to the Advent of Code Program!");

int year = ReadYear("Choose which year you want and we will check if we have solutions for this year (Press enter for current year)");

StartDayFromYear(year, "Now please choose a day (1-25) to pick a solution for this year (Press enter for current day)");

Console.ReadLine();

int ReadYear(string prompt)
{
    while (true)
    {
        Console.WriteLine(prompt);
        string input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            input = DateTime.Now.Year.ToString();

        if (int.TryParse(input, out int year))
        {
            string[] yearDirectories = Directory.GetDirectories("..\\..\\..\\Years");
            int[] years = yearDirectories.Select(path => int.Parse(new DirectoryInfo(path).Name)).ToArray();

            if (Array.IndexOf(years, year) != -1)
                return year;
        }

        Console.WriteLine("We have no solution for this year, Trying to create structure");

        Directory.CreateDirectory($"..\\..\\..\\Years\\{year}");
        Directory.CreateDirectory($"..\\..\\..\\Years\\{year}\\Days");
        Directory.CreateDirectory($"..\\..\\..\\Years\\{year}\\Inputs");

        return year;
    }
}

void StartDayFromYear(int year, string prompt)
{
    bool stop = false;

    while (!stop)
    {
        Console.WriteLine(prompt);

        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
            input = DateTime.Now.Day.ToString();

        if (int.TryParse(input, out int day) && day > 0 && day <= 25)
            stop = StartSolution(year, day);

        if (!stop)
        {
            Console.WriteLine($"We have no solution for this day. Trying to create files for day {day}");
            DayCreator.CreateDay(year, day);
            InputCreator.CreateInput(year, day, out stop);

            if (stop)
                Console.WriteLine($"Good luck with day {day}!");
        }
        else
        {
            Console.WriteLine("We have no solution for this year");
        }
    }
}

bool StartSolution(int year, int day)
{
    ISolution solution = GetInstance($"AdventOfCode._{year}.Day{day}");

    if (solution != null)
    {
        solution.Run();
        return true;
    }

    return false;
}

ISolution GetInstance(string strFullyQualifiedName)
{
    Type t = Type.GetType(strFullyQualifiedName);
    try
    {
        return (ISolution)Activator.CreateInstance(t);
    }
    catch
    {
        return null;
    }
}
