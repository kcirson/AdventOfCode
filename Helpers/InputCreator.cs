using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode.Helpers;

internal static class InputCreator
{
    public static void CreateInput(int year, int day, out bool success)
    {
        IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

        success = false;
        string content;
        Uri baseAddress = new("https://adventofcode.com/");
        string cookieValue = config["AoCSessionKey"];
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
            File.WriteAllText(InputHelper.GetFilePath(year, day), content);
        }
    }
}
