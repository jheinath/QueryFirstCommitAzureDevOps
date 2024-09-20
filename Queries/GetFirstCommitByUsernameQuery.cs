using QueryFirstCommitAzureDevOps.Repositories;

namespace QueryFirstCommitAzureDevOps.Queries;

public class GetFirstCommitByUsernameQuery(IAzureDevOpsRepository azureDevOpsRepository)
    : IGetFirstCommitByUsernameQuery
{
    public async Task<string> ExecuteAsync(string username, bool onlyMasterOrMain)
    {
        var projects = await azureDevOpsRepository.GetAllProjectsAsync();
        Console.WriteLine(string.Join(",", projects.Select(x => $"{x.Item1}, {x.Item2}")));
        return "id";
    }
}