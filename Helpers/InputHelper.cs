using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class InputHelper
    {
        public static List<string> GetInput(int year, int day) =>
            System.IO.File.ReadAllLines($"..\\..\\..\\{year}\\Inputs\\Day{day}Input.txt").ToList();

        public static string GetInputString(int year, int day) =>
            System.IO.File.ReadAllText($"..\\..\\..\\{year}\\Inputs\\Day{day}Input.txt");
    }
}
