using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day11
    {
        private static List<string> Input =>
            InputHelper.GetInput(2020, 11);

        public static void Run()
        {
            Console.WriteLine("Part 1:");
            Console.WriteLine(Part1());
            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine(Part2());
        }

        private static int Part1()
        {
            Room room = new Room(Input);

            return 0;
        }

        private static int Part2()
        {
            return 0;
        }

        public class Room
        {
            public List<Row> Rows { get; set; } = new List<Row>();
            public int RowCount { get; set; }

            public Room(List<string> floorPlan)
            {
                RowCount = floorPlan.Count;

                for(int i = 0; i < RowCount; i++)
                {
                    Rows.Add(new Row(i, floorPlan[i]));
                }
            }

            public void Changes()
            {
                List<Row> temp = new List<Row>(Rows);

                for(int i = 0; i < RowCount; i++)
                {
                    Row rowAbove = i != -1 ? temp[i - 1] : null;
                    Row currentRow = temp[i];
                    Row rowBelow = i < RowCount ? temp[i] : null; 

                    int seatCount = currentRow.AmountOfSeats;

                    for(int j = 0; j < seatCount; j++)
                    {
                        if(rowAbove != null)
                        {
                            
                        }

                        if(rowBelow != null)
                        {

                        }
                    }
                }
            }
        }

        public class Row
        {
            public int RowNumber { get; set; }
            public List<Seat> Seats { get; set; } = new List<Seat>();
            public int AmountOfSeats { get; set; }

            public Row(int row, string seats)
            {
                RowNumber = row;

                foreach(char c in seats)
                {
                    Seats.Add(new Seat(c));
                }

                AmountOfSeats = Seats.Count;
            }
        }

        public class Seat
        {
            public char CurrentState { get; set; }
            public bool CanChange => CurrentState != '.';

            public Seat(char c)
            {
                CurrentState = c;
            }
        }
    }
}
