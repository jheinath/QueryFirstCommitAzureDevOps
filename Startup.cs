using QueryFirstCommitAzureDevOps.Queries;

namespace QueryFirstCommitAzureDevOps;

public class Startup(IGetFirstCommitsByUserEmailQuery getFirstCommitsByUserEmailQuery)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var userEmail = GetUsernameFromConsole();
        var amountOfCommits = AmountOfCommitsFromConsole();
        var firstCommits = await getFirstCommitsByUserEmailQuery.ExecuteAsync(userEmail, amountOfCommits);
        OutputFirstCommits(firstCommits);
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

    private static void OutputFirstCommits(IEnumerable<(string, DateTime)> firstCommits)
    {
        foreach (var commit in firstCommits)
        {
            Console.WriteLine($"{commit.Item2} - {commit.Item1}");
        }
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}