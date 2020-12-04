using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public class Year2019
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
                default:
                    return false;
            }
        }
    }
}
