namespace AdventOfCode._2019;

public class Day3 : ISolution
{
    private static List<string> Input =>
        InputHelper.GetListString(2019, 3);

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
        Dimension dim = new(Input);

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
        Dimension dim = new(Input);

        int smallestSteps = 0;

        var crossedPoints = dim.FindStepsToCrossedPoint();

        foreach (var pair in crossedPoints)
        {
            if (pair.Key.StepsToThisPoint > 0)
            {
                int steps = pair.Key.StepsToThisPoint + pair.Value.StepsToThisPoint;

                if (smallestSteps == 0)
                    smallestSteps = steps;

                if (steps < smallestSteps)
                    smallestSteps = steps;
            }
        }

        return smallestSteps;
    }

    private class Dimension
    {
        public List<Point> Line1 = new();
        public List<Point> Line2 = new();

        public Dimension(List<string> lines)
        {
            MarkLines(lines[0], 1);
            MarkLines(lines[1], 2);
        }

        public void MarkLines(string line, int type)
        {
            Point lastPoint = new(0, 0, 0, type);
            int stepCount = 1;
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
                                lastPoint = new Point(lastPoint.GetX(), lastPoint.GetY() + 1, stepCount, type);
                                AddPoint(lastPoint);
                                stepCount++;
                            }
                            break;
                        case 'D':
                            for (int i = 1; i <= steps; i++)
                            {
                                lastPoint = new Point(lastPoint.GetX(), lastPoint.GetY() - 1, stepCount, type);
                                AddPoint(lastPoint);
                                stepCount++;
                            }
                            break;
                        case 'L':
                            for (int i = 1; i <= steps; i++)
                            {
                                lastPoint = new Point(lastPoint.GetX() - 1, lastPoint.GetY(), stepCount, type);
                                AddPoint(lastPoint);
                                stepCount++;
                            }
                            break;
                        case 'R':
                            for (int i = 1; i <= steps; i++)
                            {
                                lastPoint = new Point(lastPoint.GetX() + 1, lastPoint.GetY(), stepCount, type);
                                AddPoint(lastPoint);
                                stepCount++;
                            }
                            break;
                    }
                }
            }
        }

        public void AddPoint(Point point)
        {
            if (point.Type == 1)
                Line1.Add(point);
            else if (point.Type == 2)
                Line2.Add(point);
        }

        public List<Point> FindCrossedPoints()
        {
            return Line1.Intersect(Line2).ToList();
        }

        public Dictionary<Point, Point> FindStepsToCrossedPoint()
        {
            Dictionary<Point, Point> result = new();

            var list1 = Line1.Intersect(Line2).ToList();
            var list2 = Line2.Intersect(Line1).ToList();

            foreach (var point in list1)
            {
                if (list2.Find(p => p.X == point.X && p.Y == point.Y) is Point found)
                {
                    result.Add(point, found);
                }
            }

            return result;
        }
    }

    private class Point : IEquatable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int StepsToThisPoint { get; set; }
        public int Type { get; set; }

        public Point(int x, int y, int steps, int type)
        {
            X = x;
            Y = y;
            StepsToThisPoint = steps;
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
