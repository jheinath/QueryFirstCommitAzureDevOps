using QueryFirstCommitAzureDevOps.Queries;

namespace QueryFirstCommitAzureDevOps;

public class Startup(IGetFirstCommitByUserEmailQuery getFirstCommitByUserEmailQuery)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var userEmail = GetUsernameFromConsole();
        var onlyMasterOrMain = GetShouldOnlySearchMaster();

        var firstCommitId = await getFirstCommitByUserEmailQuery.ExecuteAsync(userEmail, onlyMasterOrMain);
        Console.WriteLine(userEmail);
        Console.WriteLine(onlyMasterOrMain);
        Console.WriteLine(firstCommitId);
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
    
    private static bool GetShouldOnlySearchMaster()
    {
        while (true)
        {
            Console.Write("Should only commits to master or main be included? (y/n): ");
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