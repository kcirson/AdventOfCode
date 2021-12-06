using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020;

public class Day5 : ISolution
{
    private static List<string> Input =>
            InputHelper.GetInput(2020, 5);

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
        Plane plane = new(128, 8);
        List<int> seatIds = Input.Select(s => plane.CheckSeat(s)).ToList();

        return seatIds.Max();
    }
    private static int Part2()
    {
        Plane plane = new(128, 8);
        List<int> seatIds = Input.Select(s => plane.CheckSeat(s)).ToList();

        return Enumerable.Range(0, seatIds.Max()).Except(seatIds).Max();
    }

    private class Plane
    {
        private int RowsOfSeats { get; set; }
        private int SeatsPerRow { get; set; }

        public Plane(int rows, int seats)
        {
            RowsOfSeats = rows;
            SeatsPerRow = seats;
        }

        public int CheckSeat(string boardingPass)
        {
            int minRow = 0;
            int maxRow = RowsOfSeats - 1;

            for (int i = 0; i < 7; i++)
            {
                switch (boardingPass[i])
                {
                    case 'F':
                        maxRow = (int)Math.Floor((minRow + maxRow) / 2.0);
                        break;
                    case 'B':
                        minRow = (int)Math.Floor((minRow + maxRow) / 2.0 + 1);
                        break;
                }
            }

            int minSeat = 0;
            int maxSeat = SeatsPerRow - 1;

            for (int i = 7; i < boardingPass.Length; i++)
            {
                switch (boardingPass[i])
                {
                    case 'R':
                        minSeat = (int)Math.Floor((minSeat + maxSeat) / 2.0 + 1);
                        break;
                    case 'L':
                        maxSeat = (int)Math.Floor((minSeat + maxSeat) / 2.0);
                        break;
                }
            }

            return minRow * SeatsPerRow + minSeat;
        }
    }
}
