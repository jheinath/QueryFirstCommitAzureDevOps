using Application.Queries.Interfaces;

namespace QueryFirstCommitAzureDevOps;

public class Startup(IGetFirstCommitsByUserEmailQuery getFirstCommitsByUserEmailQuery)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var continueSearching = true;
        while (continueSearching)
        {
            var userEmail = GetUsernameFromConsole();
            var amountOfCommits = AmountOfCommitsFromConsole();
            var firstCommits = (await getFirstCommitsByUserEmailQuery.ExecuteAsync(userEmail, amountOfCommits)).ToList();
            OutputFirstCommits(firstCommits);
            continueSearching = ContinueWithAnotherSearch();
        }
    }

    private static string GetUsernameFromConsole()
    {
        while (true)
        {
            Console.Write("Insert user email to query first commit: ");
            var userEmail = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userEmail))
                continue;
            return userEmail;
        }
    }
    
    private static int AmountOfCommitsFromConsole()
    {
        while (true)
        {
            Console.Write("Insert amount of first commits you want to receiver (e.g. 10): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out var amount))
                continue;
            return amount;
        }
    }

    private static void OutputFirstCommits(IList<(string, DateTime)> firstCommits)
    {
        if (!firstCommits.Any())
            Console.WriteLine("No commits for the user with configuration.");
        
        foreach (var commit in firstCommits)
        {
            Console.WriteLine($"{commit.Item2} - {commit.Item1}");
        }
    }
    
    private static bool ContinueWithAnotherSearch()
    {
        while (true)
        {
            Console.Write("Want to do another query? (y/n): ");
            var yesOrNo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(yesOrNo))
                continue;
            switch (yesOrNo.ToLower())
            {
                case "y":
                    return true;
                case "n":
                    return false;
                default:
                    continue;
            }
        }
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}