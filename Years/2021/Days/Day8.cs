namespace AdventOfCode._2021;
public class Day8 : ISolution
{
    private static List<string> Input =>
         InputHelper.GetListString(2021, 8);

    //private static List<string> Input =>
    //    new()
    //    {
    //        "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
    //        "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc",
    //        "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
    //        "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
    //        "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
    //        "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
    //        "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
    //        "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
    //        "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
    //        "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
    //    };

    //private static List<string> Input =>
    //    new()
    //    {
    //        "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"
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
        List<Number> numbers = CreateNumberList();
        int[] numbersToCheck = new[] { 1, 4, 7, 8 };

        return CheckInput(Input, numbers.Where(n => numbersToCheck.Contains(n.Value)).ToList());
    }

    private static int Part2()
    {
        return CheckInput2(Input, CreateNumberList());
    }

    private static int CheckInput(List<string> input, List<Number> numbers)
    {
        List<Number> found = new();

        foreach (string s in input)
        {
            string[] entry = s.Split('|');

            string[] signals = entry[0].Split(' ');
            string[] output = entry[1].Split(' ');

            foreach (string signal in output)
            {
                if (numbers.FirstOrDefault(n => n.SegmentCount == signal.Length) is Number number)
                {
                    found.Add(number);
                }
            }
        }

        return found.Count;
    }

    private static int CheckInput2(List<string> input, List<Number> numbers)
    {
        int total = 0;

        foreach (string s in input)
        {
            string[] entry = s.Split('|');

            string[] signals = entry[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] outputs = entry[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            List<string> remainingSignals = new(signals);

            if (remainingSignals.First(s => s.Length == 2) is string foundSignalOne)
            {
                Number found = numbers.Find(n => n.Value == 1);
                found.DecodedSegments = foundSignalOne.ToCharArray();
                remainingSignals.Remove(foundSignalOne);
            }

            if (remainingSignals.First(s => s.Length == 3) is string foundSignalSeven)
            {
                Number found = numbers.Find(n => n.Value == 7);
                found.DecodedSegments = foundSignalSeven.ToCharArray();
                remainingSignals.Remove(foundSignalSeven);
            }

            if (remainingSignals.First(s => s.Length == 4) is string foundSignalFour)
            {
                Number found = numbers.Find(n => n.Value == 4);
                found.DecodedSegments = foundSignalFour.ToCharArray();
                remainingSignals.Remove(foundSignalFour);
            }

            if (remainingSignals.First(s => s.Length == 7) is string foundSignalEight)
            {
                Number found = numbers.Find(n => n.Value == 8);
                found.DecodedSegments = foundSignalEight.ToCharArray();
                remainingSignals.Remove(foundSignalEight);
            }

            Number one = numbers.Find(n => n.Value == 1);
            Number eight = numbers.Find(n => n.Value == 8);

            if (remainingSignals.First(sig => eight.DecodedSegments.Except(sig.ToCharArray()).Count() == 1 &&
                                        sig.ToCharArray().Intersect(one.DecodedSegments).Count() == 1) is string foundSignalSix)
            {
                Number found = numbers.Find(n => n.Value == 6);
                found.DecodedSegments = foundSignalSix.ToCharArray();

                remainingSignals.Remove(foundSignalSix);
            }

            Number four = numbers.Find(n => n.Value == 4);

            if (remainingSignals.First(sig => eight.DecodedSegments.Except(sig.ToCharArray()).Count() == 1 &&
                                        sig.ToCharArray().Intersect(four.DecodedSegments).Count() == four.SegmentCount) is string foundSignalNine)
            {
                Number found = numbers.Find(n => n.Value == 9);
                found.DecodedSegments = foundSignalNine.ToCharArray();
                remainingSignals.Remove(foundSignalNine);
            }

            if (remainingSignals.First(sig => eight.DecodedSegments.Except(sig.ToCharArray()).Count() == 1) is string foundSignalZero)
            {
                Number found = numbers.Find(n => n.Value == 0);
                found.DecodedSegments = foundSignalZero.ToCharArray();
                remainingSignals.Remove(foundSignalZero);
            }

            Number six = numbers.Find(n => n.Value == 6);

            if (remainingSignals.First(sig => six.DecodedSegments.Except(sig.ToCharArray()).Count() == 1) is string foundSignalFive)
            {
                Number found = numbers.Find(n => n.Value == 5);
                found.DecodedSegments = foundSignalFive.ToCharArray();
                remainingSignals.Remove(foundSignalFive);
            }

            if (remainingSignals.First(sig => eight.DecodedSegments.Except(sig.ToCharArray()).Count() == 2 &&
                                    sig.ToCharArray().Intersect(one.DecodedSegments).Count() == 2) is string foundSignalThree)
            {
                Number found = numbers.Find(n => n.Value == 3);
                found.DecodedSegments = foundSignalThree.ToCharArray();
                remainingSignals.Remove(foundSignalThree);
            }

            if (remainingSignals.First(sig => eight.DecodedSegments.Except(sig.ToCharArray()).Count() == 2) is string foundSignalTwo)
            {
                Number found = numbers.Find(n => n.Value == 2);
                found.DecodedSegments = foundSignalTwo.ToCharArray();
                remainingSignals.Remove(foundSignalTwo);
            }

            int[] outputData = new int[outputs.Length];

            for (int i = 0; i < outputData.Length; i++)
            {
                char[] outputChars = outputs[i].ToCharArray();

                foreach (Number n in numbers)
                {
                    if (n.DecodedSegments.Length == outputChars.Length && n.DecodedSegments.Intersect(outputChars).Count() == outputChars.Length)
                    {
                        outputData[i] = n.Value;
                    }
                }
            }

            int value = int.Parse(string.Join("", outputData));
            total += value;
        }

        return total;
    }

    private static List<Number> CreateNumberList()
    {
        List<Number> numbers = new();

        numbers.Add(new Number(0, "abcefg"));
        numbers.Add(new Number(1, "cf"));
        numbers.Add(new Number(2, "acdeg"));
        numbers.Add(new Number(3, "acdfg"));
        numbers.Add(new Number(4, "bcdf"));
        numbers.Add(new Number(5, "abdfg"));
        numbers.Add(new Number(6, "abdefg"));
        numbers.Add(new Number(7, "acf"));
        numbers.Add(new Number(8, "abcdefg"));
        numbers.Add(new Number(9, "abcdfg"));

        return numbers;
    }
}

public class Number
{
    public int Value { get; set; }
    public char[] Segments { get; set; }
    public char[] DecodedSegments { get; set; }
    public int SegmentCount { get { return Segments.Length; } }

    public Number(int number, string input)
    {
        Value = number;
        Segments = input.ToArray();
    }

    public override string ToString()
    {
        return string.Join("", Segments);
    }
}