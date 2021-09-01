using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPIClient
{

    /*************************************************************************
    * TUTORIAL: MAKE HTTP REQUESTS IN A.NET CONSOLE APP USING C# 
    * https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
    *
    *
    ************************************************************************/

    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            var repositories = await ProcessRepositories();

            static async Task<List<Repository>> ProcessRepositories()
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
                var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

                foreach (var repo in repositories)
                {
                    Console.WriteLine(repo.Name);
                    Console.WriteLine(repo.Description);
                    Console.WriteLine($"GitHub home URL: {repo.GitHubHomeUrl}");
                    Console.WriteLine($"URL homepage: {repo.Homepage}");
                    Console.WriteLine($"Watchers: {repo.Watchers}");
                    Console.WriteLine(repo.LastPushUtc);
                    Console.WriteLine();
                }

                return repositories;
            }
        }
    }
}
