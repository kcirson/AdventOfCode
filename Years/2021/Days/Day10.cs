namespace AdventOfCode._2021;
public class Day10 : ISolution
{
    private static List<string> Input =>
         InputHelper.GetInput(2021, 10).ToList();

    //private static List<string> Input =>
    //    new()
    //    {
    //        "[({(<(())[]>[[{[]{<()<>>",
    //        "[(()[<>])]({[<{<<[]>>(",
    //        "(((({<>}<{<{<>}{[]{[]{}",
    //        "{<[[]]>}<{[{[{[]{()[[[]",
    //        "<{([{{}}[<[[[<>{}]]]>[]]"
    //    };

    private static Dictionary<char, int> IllegalCharacterPoints = new()
    {
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 },
    };

    private static Dictionary<char, int> AutoCompletePoints = new()
    {
        { ')', 1 },
        { ']', 2 },
        { '}', 3 },
        { '>', 4 },
    };

    private static Dictionary<char, char> Tokens = new()
    {
        { ')', '(' },
        { ']', '[' },
        { '}', '{' },
        { '>', '<' },
    };

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
        int points = 0;

        foreach (string s in Input)
        {
            points += CheckLine(s);
        }

        return points;
    }

    private static long Part2()
    {
        List<long> scores = new();

        foreach (string s in Input)
        {
            long score = CheckLine2(s);
            if (score > 0)
            {
                scores.Add(score);
            }
        }

        scores.Sort();
        long points = scores.ToArray()[scores.Count / 2];

        return points;
    }

    private static int CheckLine(string line)
    {
        int points = 0;


        Stack<char> stack = new();

        foreach (char c in line)
        {
            if (Tokens.ContainsValue(c))
            {
                stack.Push(c);
            }
            else
            {
                if (stack.TryPeek(out char current))
                {
                    if (current == Tokens[c])
                        stack.Pop();
                    else
                        return IllegalCharacterPoints[c];
                }
            }
        }


        return points;
    }

    private static long CheckLine2(string line)
    {
        Stack<char> stack = new();

        foreach (char c in line)
        {
            if (Tokens.ContainsValue(c))
            {
                stack.Push(c);
            }
            else
            {
                if (stack.TryPeek(out char current))
                {
                    if (current == Tokens[c])
                        stack.Pop();
                    else
                        return 0;
                }
            }
        }

        long points = 0;

        if (stack.Count > 0)
        {
            while (stack.Count > 0)
            {
                var token = Tokens.First(t => t.Value == stack.Peek());

                points *= 5;
                points += AutoCompletePoints[token.Key];
                stack.Pop();
            }
        }

        return points;
    }
}
