using AdventOfCode._2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    public class Year2021
    {
        public static bool StartDay(int day)
        {
            switch (day)
            {
                case 1:
                    Day1.Run();
                    return true;
                default:
                    return false;
            }
        }
    }
}
