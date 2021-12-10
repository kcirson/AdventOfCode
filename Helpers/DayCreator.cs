using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers
{
    public static class DayCreator
    {
        public static void CreateDay(int year, int day)
        {
            string dayText =
                $@"using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._{year};
public class Day{day} : ISolution
{{
    //private static List<string> Input =>
    //     InputHelper.GetInput({year}, {day}).ToList();

    private static List<string> Input =>
        new()
        {{
            """",
        }};

    public void Run()
    {{
        Console.WriteLine(""Part 1:"");
        Console.WriteLine(Part1());
        Console.WriteLine();
        Console.WriteLine(""Part 2:"");
        Console.WriteLine(Part2());
    }}

    private static int Part1()
    {{
        return 1;
    }}

    private static int Part2()
    {{
        return 2;
    }}
}}";
            File.WriteAllText($"..\\..\\..\\Years\\{year}\\Days\\Day{day}.cs", dayText);
        }
    }
}
