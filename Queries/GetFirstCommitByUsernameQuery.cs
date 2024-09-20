using QueryFirstCommitAzureDevOps.Repositories;

namespace QueryFirstCommitAzureDevOps.Queries;

public class GetFirstCommitByUsernameQuery(IAzureDevOpsRepository azureDevOpsRepository)
    : IGetFirstCommitByUsernameQuery
{
    public async Task<string> ExecuteAsync(string username, bool onlyMasterOrMain)
    {
        var projects = await azureDevOpsRepository.GetAllProjectsAsync();
        Console.WriteLine(projects);
        return "moin";
    }
}