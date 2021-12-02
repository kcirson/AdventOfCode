using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

public static class InputHelper
{
    private static string cookieValue = string.Empty;

    public static List<string> GetInput(int year, int day)
    {
        bool success;
        if (!FileExists(year, day))
            GetInputFromSite(year, day, out success);
        else
            success = true;

        if (success)
            return File.ReadAllLines(GetFilePath(year, day)).ToList();

        return null;
    }

    public static string GetInputString(int year, int day)
    {
        bool success;
        if (!FileExists(year, day))
            GetInputFromSite(year, day, out success);
        else
            success = true;

        if (success)
            return File.ReadAllText(GetFilePath(year, day));

        return string.Empty;
    }

    private static bool FileExists(int year, int day) =>
        File.Exists(GetFilePath(year, day));

    private static string GetFilePath(int year, int day) =>
        $"..\\..\\..\\{year}\\Inputs\\Day{day}Input.txt";

    private static void GetInputFromSite(int year, int day, out bool success)
    {
        success = false;
        string content;
        Uri baseAddress = new("https://adventofcode.com/");
        CookieContainer cookieContainer = new();
        using (HttpClientHandler handler = new() { CookieContainer = cookieContainer })
        using (HttpClient client = new(handler) { BaseAddress = baseAddress })
        {
            if (string.IsNullOrEmpty(cookieValue))
            {
                Console.WriteLine("Please enter cookie value:");
                cookieValue = Console.ReadLine();
            }

            cookieContainer.Add(baseAddress, new Cookie("session", cookieValue));
            Task<HttpResponseMessage> task = client.GetAsync($"{year}/day/{day}/input");
            task.Wait();
            HttpResponseMessage result = task.Result;

            if (result.StatusCode == HttpStatusCode.Redirect)
            {
                Console.WriteLine("The session has expired, refresh token...");
                Console.WriteLine("Enter new cookie value:");
                cookieValue = Console.ReadLine();

                return;
            }
            else if (result.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine($"Got status code {result.StatusCode}");

                return;
            }

            Console.WriteLine("Retrieved input data...");
            Task<string> contentTask = result.Content.ReadAsStringAsync();
            contentTask.Wait();
            content = contentTask.Result.TrimEnd();
        }

        if (!string.IsNullOrEmpty(content))
        {
            success = true;
            Console.WriteLine("Creating txt file from input...");
            File.WriteAllText(GetFilePath(year, day), content);
        }
    }
}
