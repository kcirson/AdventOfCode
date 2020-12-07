using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class Day3
    {
        private static List<string> Input =>
            InputHelper.GetInput(2019, 3);

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
            Dimension dim = new Dimension(Input);

            Point closestPoint = null;
            int distance = 0;

            List<Point> crossed = dim.FindCrossedPoints();

            foreach (Point p in crossed)
            {
                if (p.GetX() == 0 && p.GetY() == 0)
                    continue;

                int manhattenDistance = Math.Abs(p.GetX()) + Math.Abs(p.GetY());

                if (closestPoint == null)
                {
                    closestPoint = p;
                    distance = manhattenDistance;
                }

                if (closestPoint != null && manhattenDistance < distance)
                {
                    closestPoint = p;
                    distance = manhattenDistance;
                }

            }

            return distance;
        }

        private static int Part2()
        {
            return 0;
        }

        private class Dimension
        {
            public List<Point> Line1 = new List<Point>();
            public List<Point> Line2 = new List<Point>();

            List<Point> Points = new List<Point>();

            public Dimension(List<string> lines)
            {
                MarkLines(lines[0], 1);
                MarkLines(lines[1], 2);
            }

            public void MarkLines(string line, int type)
            {
                Point lastPoint = new Point(0, 0, type);
                AddPoint(lastPoint);

                string[] path = line.Split(',');

                foreach (string coord in path)
                {
                    char direction = coord[0];

                    if (int.TryParse(coord[1..], out int steps))
                    {
                        switch (direction)
                        {
                            case 'U':
                                for (int i = 1; i <= steps; i++)
                                {
                                    lastPoint = new Point(lastPoint.GetX(), lastPoint.GetY() + 1, type);
                                    AddPoint(lastPoint);
                                }
                                break;
                            case 'D':
                                for (int i = 1; i <= steps; i++)
                                {
                                    lastPoint = new Point(lastPoint.GetX(), lastPoint.GetY() - 1, type);
                                    AddPoint(lastPoint);
                                }
                                break;
                            case 'L':
                                for (int i = 1; i <= steps; i++)
                                {
                                    lastPoint = new Point(lastPoint.GetX() - 1, lastPoint.GetY(), type);
                                    AddPoint(lastPoint);
                                }
                                break;
                            case 'R':
                                for (int i = 1; i <= steps; i++)
                                {
                                    lastPoint = new Point(lastPoint.GetX() + 1, lastPoint.GetY(), type);
                                    AddPoint(lastPoint);
                                }
                                break;
                        }
                    }
                }
            }

            public void AddPoint(int x, int y, int type)
            {
                AddPoint(new Point(x, y, type));
            }

            public void AddPoint(Point point)
            {
                if (point.Type == 1)
                    Line1.Add(point);
                else if (point.Type == 2)
                    Line2.Add(point);

                Points.Add(point);
            }

            public List<Point> FindCrossedPoints()
            {
                return Line1.Intersect(Line2).ToList();
            }
        }

        private class Point : IEquatable<Point>
        {
            public int X { get; set; }
            public int Y { get; set; }

            public int Type { get; set; }

            public Point(int x, int y, int type)
            {
                X = x;
                Y = y;
                Type = type;
            }

            public int GetX() => X;
            public int GetY() => Y;

            public bool Equals(Point other)
            {
                return X == other.X && Y == other.Y;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Point);
            }

            public override int GetHashCode()
            {
                return X.GetHashCode() + Y.GetHashCode();
            }
        }
    }
}
