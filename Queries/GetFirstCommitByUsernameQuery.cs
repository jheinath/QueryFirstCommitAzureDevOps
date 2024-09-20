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
        Console.WriteLine(string.Join(",", repos.GitRepositories.Select(x => $"{x.CollectionName}:{x.ProjectName}:{x.GitRepositoryId}")));
        return "id";
    }
}