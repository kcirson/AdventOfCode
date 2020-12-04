using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2020
{
    class Day4
    {
        private static List<string> Input =>
                InputHelper.GetInputString(2020, 4).Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.None).ToList();

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
            List<Passport> passports = GetPassports(Input);

            return passports.Count(p => p.IsValid);
        }
        private static int Part2()
        {
            return 0;
        }

        private static List<Passport> GetPassports(List<string> data)
        {
            int count = data.Count;
            List<Passport> passports = new List<Passport>();

            Passport passport = new Passport();

            for (int i = 0; i < count; i++)
            {
                if (!string.IsNullOrEmpty(data[i]))
                {
                    passport.SetValue(data[i]);
                }
                else
                {
                    passports.Add(passport);
                    passport = new Passport();
                }
            }

            return passports;
        }

        private class Passport
        {
            string BirthYear { get; set; } // byr
            string IssueYear { get; set; } //iyr
            string ExpirationYear { get; set; } //eyr
            string Height { get; set; } //hgt
            string HairColor { get; set; } // hcl
            string EyeColor { get; set; } //ecl
            string PassportID { get; set; } //pid
            string CountryID { get; set; } //cid

            public bool IsValid
            {
                get
                {
                    string[] required = new string[] { BirthYear, IssueYear, ExpirationYear, Height, HairColor, EyeColor, PassportID };
                    
                    return Array.IndexOf(required, string.Empty) == -1 && Array.IndexOf(required, null) == -1;
                }
            }

            public Passport()
            {

            }

            public void SetValue(string value)
            {
                string[] split = value.Split(':');

                switch (split[0])
                {
                    case "byr":
                        BirthYear = split[1];
                        break;
                    case "iyr":
                        IssueYear = split[1];
                        break;
                    case "eyr":
                        ExpirationYear = split[1];
                        break;
                    case "hgt":
                        Height = split[1];
                        break;
                    case "hcl":
                        HairColor = split[1];
                        break;
                    case "ecl":
                        EyeColor = split[1];
                        break;
                    case "pid":
                        PassportID = split[1];
                        break;
                    case "cid":
                        CountryID = split[1];
                        break;
                }
            }
        }
    }
}
