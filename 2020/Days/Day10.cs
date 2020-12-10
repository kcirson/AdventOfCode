using System;
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

            int oneSteps = bag.Chain.Count(ch => ch.Difference == 1);
            int threeSteps = bag.Chain.Count(ch => ch.Difference == 3);

            return oneSteps * threeSteps;
        }

        private static long Part2()
        {
            return 0;
        }

        private class AdapterBag
        {
            List<Adapter> Adapters { get; set; } = new List<Adapter>();
            public List<AdapterChain> Chain { get; set; }

            public int JoltageOutput => Adapters.Max(ad => ad.Joltage) + 3;

            public AdapterBag(List<int> ratings)
            {
                foreach (int joltage in ratings)
                {
                    Adapters.Add(new Adapter(joltage));
                }

                Adapters.Add(new Adapter(JoltageOutput));

                CreateChain(Adapters);
            }

            public void CreateChain(List<Adapter> adapters)
            {
                List<Adapter> temp = new List<Adapter>(adapters);
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
        }

        private class AdapterChain
        {
            Adapter Adapter { get; set; }
            public int Difference { get; set; }

            public AdapterChain(Adapter adapter, int diff)
            {
                Adapter = adapter;
                Difference = diff;
            }
        }
    }
}
