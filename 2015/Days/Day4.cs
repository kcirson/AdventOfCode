using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015;

public static class Day4
{
    private static string Input => "bgvyzdsv";

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
        return CalculateSalt(5);
    }

    private static int Part2()
    {
        return CalculateSalt(6);
    }

    private static int CalculateSalt(int startingZeroes)
    {
        int salt = 0;

        Regex regex = new($"^0{{{startingZeroes}}}");

        while (true)
        {
            string md5 = CreateMD5($"{Input}{salt}");

            if (regex.Match(md5).Success)
                break;

            salt++;
        }

        return salt;
    }

    public static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using MD5 md5 = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        // Convert the byte array to hexadecimal string
        StringBuilder sb = new();
        for (int i = 0; i < hashBytes.Length; i++)
            sb.Append(hashBytes[i].ToString("X2"));

        return sb.ToString();
    }
}
