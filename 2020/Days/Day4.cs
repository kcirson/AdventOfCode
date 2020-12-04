using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            List<Passport> passports = GetPassports(Input);

            return passports.Count(p => p.IsValid2);
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
            public string PassportID { get; set; } //pid
            private string CountryID { get; set; } //cid

            public bool IsValid
            {
                get
                {
                    string[] required = new string[] { BirthYear, IssueYear, ExpirationYear, Height, HairColor, EyeColor, PassportID };

                    return Array.IndexOf(required, string.Empty) == -1 && Array.IndexOf(required, null) == -1;
                }
            }

            public bool IsValid2
            {
                get
                {
                    if (!IsValid)
                        return false;

                    if (!CheckNumbers(BirthYear, 1920, 2002))
                        return false;

                    if (!CheckNumbers(IssueYear, 2010, 2020))
                        return false;

                    if (!CheckNumbers(ExpirationYear, 2020, 2030))
                        return false;

                    if (!CheckHeight(Height))
                        return false;

                    if (!CheckEyeColor(EyeColor))
                        return false;

                    if (!CheckHairColor(HairColor))
                        return false;

                    if (!CheckPassportID(PassportID))
                        return false;

                    return true;

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

            private bool CheckNumbers(string value, int min, int max)
            {
                if (int.TryParse(value, out int number))
                {
                    return number >= min && number <= max;
                }
                else
                    return false;
            }

            private bool CheckHeight(string value)
            {
                if (value.IndexOf("cm") > -1 || value.IndexOf("in") > -1)
                {
                    string heightType = value.Substring(value.Length - 2, 2);
                    string height = value.Replace("cm", "").Replace("in", "");

                    if (int.TryParse(height, out int intHeight))
                    {
                        switch (heightType)
                        {
                            case "cm":
                                return intHeight >= 150 && intHeight <= 193;
                            case "in":
                                return intHeight >= 59 && intHeight <= 76;
                        }
                    }
                }
                return false;
            }

            private bool CheckEyeColor(string value)
            {
                string[] validColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

                return Array.IndexOf(validColors, value) != -1;
            }

            private bool CheckHairColor(string value)
            {
                return Regex.Match(value, "^[#][0-9a-f]{6}$").Success;
            }

            private bool CheckPassportID(string passportID)
            {
                return Regex.Match(passportID, "^[0-9]{9}$").Success;
            }
        }
    }
}
