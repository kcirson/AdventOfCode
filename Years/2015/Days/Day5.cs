using System.Text.RegularExpressions;

namespace AdventOfCode._2015;

public class Day5 : ISolution
{
    private static List<string> Input =>
        InputHelper.GetListString(2015, 5);

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
        int niceStrings = 0;
        Regex vowels = new("[aeiou]");
        Regex doubleCharacters = new("(\\w)\\1{1}");

        foreach (string input in Input)
        {
            if (input.Contains("ab") || input.Contains("cd") || input.Contains("pq") || input.Contains("xy"))
                continue;

            var vowelMatches = vowels.Matches(input);

            if (vowelMatches.Count < 3)
                continue;

            var dchars = doubleCharacters.Matches(input);

            if (dchars.Count > 0)
                niceStrings++;
        }

        return niceStrings;
    }

    private static int Part2()
    {
        int niceStrings = 0;

        foreach (string input in Input)
        {
            if (FindPair(input) && FindRepeatingLetter(input))
                niceStrings++;
        }

        bool FindPair(string input)
        {
            char[] chars = input.ToCharArray();
            var length = chars.Length;

            for (int i = 0; i < length; i++)
            {
                char current = chars[i];
                int nextIndex = i + 1;
                int nextNextIndex = i + 2;

                if (nextIndex >= length)
                    continue;

                char next = chars[nextIndex];
                string toFind = $"{current}{next}";
                string subString = input[nextNextIndex..];

                if (subString.Contains(toFind))
                    return true;
            }

            return false;
        }

        bool FindRepeatingLetter(string input)
        {
            char[] chars = input.ToCharArray();
            var length = chars.Length;

            for (int i = 0; i < length; i++)
            {
                char current = chars[i];
                int nextNextIndex = i + 2;

                if (nextNextIndex >= length)
                    continue;

                char nextNext = chars[nextNextIndex];

                if (current == nextNext)
                    return true;
            }

            return false;
        }

        return niceStrings;
    }
}
