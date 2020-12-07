using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day7
    {
        private static List<string> Input =>
            InputHelper.GetInput(2020, 7);

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
            List<Bag> bags = new List<Bag>();

            foreach (string s in Input)
            {
                string[] split = s.Split(new string[] { "bags contain", ",", "." }, StringSplitOptions.RemoveEmptyEntries);
                int count = split.Length;
                Bag bag = new Bag(split[0]);
                List<Bag> subBags = new List<Bag>();

                for (int i = 1; i < count; i++)
                {
                    string[] subBagSplit = split[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (int.TryParse(subBagSplit[0], out int amount))
                    {
                        for (int j = 0; j < amount; j++)
                            subBags.Add(new Bag(string.Join(' ', subBagSplit[1..])));
                    }

                }

                bag.AddBags(subBags);

                if (bags.FirstOrDefault(b => b.Color == split[0]) is Bag foundBag)
                {
                    foundBag.AddBags(subBags);
                }
                else
                {
                    bags.Add(bag);
                }

            }

            List<Bag> CanHoldShinyBag = FindBagsThatCouldContainColor(bags, "shiny gold");

            return CanHoldShinyBag.Select(b => b.Color).Distinct().Count();
        }

        private static int Part2()
        {
            List<Bag> bags = new List<Bag>();

            foreach (string s in Input)
            {
                string[] split = s.Split(new string[] { "bags contain", ",", "." }, StringSplitOptions.RemoveEmptyEntries);
                int count = split.Length;
                Bag bag = new Bag(split[0]);
                List<Bag> subBags = new List<Bag>();

                for (int i = 1; i < count; i++)
                {
                    string[] subBagSplit = split[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (int.TryParse(subBagSplit[0], out int amount))
                    {
                        for (int j = 0; j < amount; j++)
                            subBags.Add(new Bag(string.Join(' ', subBagSplit[1..])));
                    }

                }

                bag.AddBags(subBags);

                if (bags.FirstOrDefault(b => b.Color == split[0]) is Bag foundBag)
                {
                    foundBag.AddBags(subBags);
                }
                else
                {
                    bags.Add(bag);
                }

            }

            List<Bag> ShinyBagContains = FindBagsThatAreInAColoredBag(bags, "shiny gold");

            return ShinyBagContains.Count;
        }

        public class Bag : IEquatable<Bag>
        {
            private readonly string _Color;

            public string Color
            {
                get
                {
                    return _Color;
                }
            }

            public List<Bag> ContainsBags { get; set; } = new List<Bag>();

            public Bag(string color)
            {
                _Color = color.Replace("bags", "").Replace("bag", "").Trim();
            }

            public void AddBags(List<Bag> bags)
            {
                ContainsBags.AddRange(bags);
            }

            public bool Equals(Bag other)
            {
                return Color == other.Color;
            }

            public bool HasBag(string bagColor)
            {
                return ContainsBags.Find(b => b.Color == bagColor) != null;
            }
        }

        private static List<Bag> FindBagsThatCouldContainColor(List<Bag> bags, string color)
        {
            List<Bag> containsColor = bags.Where(b => b.HasBag(color)).ToList();

            List<Bag> foundBags = new List<Bag>();

            ConcurrentQueue<Bag> queue = new ConcurrentQueue<Bag>(containsColor);

            while (queue.Count > 0)
            {
                if (queue.TryDequeue(out Bag bag))
                {
                    foundBags.Add(bag);

                    var moreBags = bags.Where(b => b.HasBag(bag.Color));

                    Parallel.ForEach(moreBags, anotherBag => { queue.Enqueue(anotherBag); });
                }
            }

            return foundBags;
        }

        private static List<Bag> FindBagsThatAreInAColoredBag(List<Bag> bags, string color)
        {
            List<Bag> containsColor = bags.Where(b => b.Color == color).ToList();
            List<Bag> foundBags = new List<Bag>();

            ConcurrentQueue<Bag> queue = new ConcurrentQueue<Bag>(containsColor.SelectMany(b => b.ContainsBags));

            while (!queue.IsEmpty)
            {
                if (queue.TryDequeue(out Bag bag))
                {
                    foundBags.Add(bag);

                    var moreBags = bags.Where(b => b.Color == bag.Color);

                    Parallel.ForEach(moreBags, anotherBag =>
                    {
                        Parallel.ForEach(anotherBag.ContainsBags, subBag => { queue.Enqueue(subBag); });
                    });
                }
            }

            return foundBags;
        }
    }
}
