using QueryFirstCommitAzureDevOps.Repositories;

namespace QueryFirstCommitAzureDevOps.Queries;

public class GetFirstCommitByUsernameQuery(IAzureDevOpsRepository azureDevOpsRepository)
    : IGetFirstCommitByUsernameQuery
{
    public async Task<string> ExecuteAsync(string username, bool onlyMasterOrMain)
    {
        var projects = await azureDevOpsRepository.GetAllProjectsAsync();
        Console.WriteLine(string.Join(",", projects.Projects.Select(x => $"{x.CollectionName}, {x.ProjectName}")));
        var repos = await azureDevOpsRepository.GetAllGitRepositories(projects);
        foreach (var test in repos.Projects)
        {
            Console.WriteLine(string.Join(",", test.GitRepositoryIds.Select(x => $"{test.CollectionName}:{test.ProjectName}:{x}")));
        }
        return "id";
    }
}