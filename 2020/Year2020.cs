using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode._2020
{
    public class Year2020
    {
        public Year2020(int day)
        {
            switch (day)
            {
                case 1:
                    Day1.Run();
                    break;
                case 2:
                    Day2.Run();
                    break;
                case 3:
                    Day3.Run();
                    break;
                case 4:
                    Day4.Run();
                    break;
            }
        }
    }
}
