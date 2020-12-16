using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day10
    {
        private static List<int> Input =>
            InputHelper.GetInput(2020, 10).Select(int.Parse).ToList();

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
            AdapterBag bag = new AdapterBag(Input);
            bag.CreateChain();
            int oneSteps = bag.Chain.Count(ch => ch.Difference == 1);
            int threeSteps = bag.Chain.Count(ch => ch.Difference == 3);

            return oneSteps * threeSteps;
        }

        private static long Part2()
        {
            AdapterBag bag = new AdapterBag(Input);
            bag.CheckAllPossbileChains();

            return bag.PossibleChain[bag.JoltageOutput].GetPathsToZero();
        }

        private class AdapterBag
        {
            public List<Adapter> Adapters { get; set; } = new List<Adapter>();
            public List<AdapterChain> Chain { get; set; }
            public Dictionary<long, AdapterChain> PossibleChain = new Dictionary<long, AdapterChain>();

            public int JoltageOutput { get; }

            public AdapterBag(List<int> ratings)
            {
                foreach (int joltage in ratings)
                {
                    Adapters.Add(new Adapter(joltage));
                }

                JoltageOutput = Adapters.Max(ad => ad.Joltage) + 3;

                Adapters.Add(new Adapter(JoltageOutput));
            }

            public void CreateChain()
            {
                List<Adapter> temp = new List<Adapter>(Adapters);
                List<AdapterChain> chain = new List<AdapterChain>();

                int joltageToSupport = 0;

                while (temp.Count != 0)
                {
                    List<Adapter> adaptersThatCanSupportJoltage = temp.FindAll(ad => ad.CanSupportJoltage(joltageToSupport));

                    Adapter found = adaptersThatCanSupportJoltage.First(ad => ad.Joltage == adaptersThatCanSupportJoltage.Min(ad => ad.Joltage));
                    chain.Add(new AdapterChain(found, found.Joltage - joltageToSupport));
                    joltageToSupport = found.Joltage;

                    temp.Remove(found);
                }

                Chain = chain;
            }

            public void CheckAllPossbileChains()
            {
                List<Adapter> orderedAdapters = new List<Adapter>(Adapters).OrderBy(a => a.Joltage).ToList();

                foreach (Adapter ad in orderedAdapters)
                    PossibleChain.Add(ad.Joltage, new AdapterChain(ad, 0));

                foreach(var joltage in PossibleChain.Keys)
                {
                    for(var j = joltage - 3; j < joltage; j++)
                    {
                        if (PossibleChain.ContainsKey(j))
                        {
                            PossibleChain[j].Children.Add(PossibleChain[j]);
                        }
                    }
                }
            }
        }

        private class Adapter : IEquatable<Adapter>, IComparable<Adapter>
        {
            private int _joltage { get; set; }
            public int Joltage => _joltage;
            private int[] SupportedJoltage { get; set; }

            public Adapter(int joltage)
            {
                _joltage = joltage;
                SupportedJoltage = Enumerable.Range(_joltage - 3, 3).ToArray();
            }

            public bool CanSupportJoltage(int joltage)
            {
                return Array.IndexOf(SupportedJoltage, joltage) != -1;
            }

            public void ChangeJoltage(int newJoltage)
            {
                _joltage = newJoltage;
            }

            public bool Equals(Adapter other)
            {
                return Joltage == other.Joltage;
            }

            public int CompareTo(Adapter other)
            {
                if (Joltage < other.Joltage)
                    return 1;

                else if (Joltage > other.Joltage)
                    return -1;

                else
                    return 0;

            }

            public override string ToString()
            {
                return Joltage.ToString();
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Adapter);
            }

            public override int GetHashCode()
            {
                return Joltage.GetHashCode();
            }
        }

        private class AdapterChain
        {
            public Adapter Adapter { get; set; }
            public int Difference { get; set; }
            public List<AdapterChain> Children { get; set; } = new List<AdapterChain>();
            public long PathsToZero = long.MinValue;

            public AdapterChain(Adapter adapter, int diff)
            {
                Adapter = adapter;
                Difference = diff;
            }

            public long GetPathsToZero()
            {
                if (PathsToZero == long.MinValue)
                {
                    PathsToZero = Children.Select(c => c.GetPathsToZero()).Sum() + (Adapter.Joltage < 4 ? 1 : 0);
                }

                return PathsToZero;
            }
        }

        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            IEnumerable<T> result = source.SelectMany(selector);

            if (!result.Any())
                return result;

            return result.Concat(result.SelectManyRecursive(selector));
        }
    }
}
