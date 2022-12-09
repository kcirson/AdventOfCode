using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers;

public static class InputHelper
{
    public static List<string> GetListString(int year, int day)
    {
        if (TryGetInput(year, day, out string[] input))
            return input.ToList();

        return null;
    }

    public static List<int> GetListInt(int year, int day)
    {
        if (TryGetInput(year, day, out string[] input))
            return input.Select(int.Parse).ToList();

        return null;
    }

    public static string GetInputString(int year, int day)
    {
        if (TryGetInput(year, day, out string input))
            return input;

        return string.Empty;
    }
    public static string GetFilePath(int year, int day) =>
        $"..\\..\\..\\Years\\{year}\\Inputs\\Day{day}Input.txt";

    private static bool TryGetInput(int year, int day, out string[] input)
    {
        bool success = false;
        input = null;

        if (FileExists(year, day))
            input = File.ReadAllLines(GetFilePath(year, day));

        return success;
    }

    private static bool TryGetInput(int year, int day, out string input)
    {
        bool success = false;
        input = null;

        if (FileExists(year, day))
            input = File.ReadAllText(GetFilePath(year, day));

        return success;
    }

    private static bool FileExists(int year, int day) =>
        File.Exists(GetFilePath(year, day));

}
