using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode._2020
{
    public class Year2020
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
                case 3:
                    Day3.Run();
                    return true;
                case 4:
                    Day4.Run();
                    return true;
                case 5:
                    Day5.Run();
                    return true;
                case 6:
                    Day6.Run();
                    return true;
                default:
                    return false;
            }
        }
    }
}
