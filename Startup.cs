using QueryFirstCommitAzureDevOps.Queries;

namespace QueryFirstCommitAzureDevOps;

public class Startup(IGetFirstCommitByUsernameQuery getFirstCommitByUsernameQuery)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var username = GetUsernameFromConsole();
        var onlyMasterOrMain = GetShouldOnlySearchMaster();

        var firstCommitId = await getFirstCommitByUsernameQuery.ExecuteAsync(username, onlyMasterOrMain);
        Console.WriteLine(username);
        Console.WriteLine(onlyMasterOrMain);
        Console.WriteLine(firstCommitId);
    }

    private static string GetUsernameFromConsole()
    {
        while (true)
        {
            Console.Write("Insert user name to query first commit: ");
            var username = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(username))
                continue;
            return username;
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