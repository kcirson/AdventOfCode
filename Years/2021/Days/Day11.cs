namespace AdventOfCode._2021;
public class Day11 : ISolution
{
    private static List<string> Input =>
         InputHelper.GetInput(2021, 11).ToList();

    //private static List<string> Input =>
    //    new()
    //    {
    //        "5483143223",
    //        "2745854711",
    //        "5264556173",
    //        "6141336146",
    //        "6357385478",
    //        "4167524645",
    //        "2176841721",
    //        "6882881134",
    //        "4846848554",
    //        "5283751526"
    //    };

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
        int count = Input.Count;
        int[][] map = new int[count][];

        for (int i = 0; i < count; i++)
        {
            map[i] = Input[i].Select(x => Convert.ToInt32(x.ToString())).ToArray();
        }

        HashSet<(int x, int y)> litOctopi = new();
        int flashCount = 0;
        for (int i = 0; i < 100; i++)
        {
            for (int x = 0; x < count; x++)
            {
                int length = map[x].Length;

                for (int y = 0; y < length; y++)
                {
                    if (!litOctopi.Contains((x, y)))
                    {
                        map[x][y]++;
                        CheckEnergyLevel(x, y, map, litOctopi);
                    }
                }
            }

            flashCount += litOctopi.Count;
            litOctopi = new();
        }


        return flashCount;
    }

    private static int Part2()
    {
        int count = Input.Count;
        int[][] map = new int[count][];

        for (int i = 0; i < count; i++)
        {
            map[i] = Input[i].Select(x => Convert.ToInt32(x.ToString())).ToArray();
        }

        HashSet<(int x, int y)> litOctopi = new();
        int flashCount = 0;

        int step = 0;
        while (true)
        {
            for (int x = 0; x < count; x++)
            {
                int length = map[x].Length;

                for (int y = 0; y < length; y++)
                {
                    if (!litOctopi.Contains((x, y)))
                    {
                        map[x][y]++;
                        CheckEnergyLevel(x, y, map, litOctopi);
                    }
                }
            }

            if (litOctopi.Count == 100)
                break;

            flashCount += litOctopi.Count;
            litOctopi = new();
            step++;
        }

        //return result to not be zerobased
        return step + 1;
    }

    private static void UpAdjecent((int x, int y) coord, int[][] map, HashSet<(int, int)> litOctopi)
    {
        int rowLength = map[coord.x].Length;
        int mapSize = map.Length;

        int left = coord.y - 1;
        int right = coord.y + 1;
        int up = coord.x - 1;
        int down = coord.x + 1;

        if (left >= 0 && left < rowLength)
        {
            if (!litOctopi.Contains((coord.x, left)))
            {
                map[coord.x][left]++;
                CheckEnergyLevel(coord.x, left, map, litOctopi);
            }

            if (up >= 0 && up < mapSize && !litOctopi.Contains((up, left)))
            {
                map[up][left]++;
                CheckEnergyLevel(up, left, map, litOctopi);
            }

            if (down >= 0 && down < mapSize && !litOctopi.Contains((down, left)))
            {
                map[down][left]++;
                CheckEnergyLevel(down, left, map, litOctopi);
            }
        }

        if (right >= 0 && right < rowLength)
        {
            if (!litOctopi.Contains((coord.x, right)))
            {
                map[coord.x][right]++;
                CheckEnergyLevel(coord.x, right, map, litOctopi);
            }

            if (up >= 0 && up < mapSize && !litOctopi.Contains((up, right)))
            {
                map[up][right]++;
                CheckEnergyLevel(up, right, map, litOctopi);
            }

            if (down >= 0 && down < mapSize && !litOctopi.Contains((down, right)))
            {
                map[down][right]++;
                CheckEnergyLevel(down, right, map, litOctopi);
            }
        }

        if (up >= 0 && up < mapSize && !litOctopi.Contains((up, coord.y)))
        {
            map[up][coord.y]++;
            CheckEnergyLevel(up, coord.y, map, litOctopi);
        }

        if (down >= 0 && down < mapSize && !litOctopi.Contains((down, coord.y)))
        {
            map[down][coord.y]++;
            CheckEnergyLevel(down, coord.y, map, litOctopi);
        }
    }

    private static void CheckEnergyLevel(int x, int y, int[][] map, HashSet<(int, int)> litOctopi)
    {
        int current = map[x][y];

        if (current > 9)
        {
            litOctopi.Add((x, y));
            map[x][y] = 0;
            UpAdjecent((x, y), map, litOctopi);
        }
    }
}
