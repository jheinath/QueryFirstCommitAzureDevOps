using QueryFirstCommitAzureDevOps.Repositories;

namespace QueryFirstCommitAzureDevOps.Queries;

public class GetFirstCommitByUserEmailQuery(IAzureDevOpsRepository azureDevOpsRepository)
    : IGetFirstCommitByUserEmailQuery
{
    public async Task<string> ExecuteAsync(string userEmail, bool onlyMasterOrMain)
    {
        var projects = await azureDevOpsRepository.GetAllProjectsAsync();
        Console.WriteLine(string.Join(",", projects.Projects.Select(x => $"{x.CollectionName}, {x.ProjectName}")));
        var repos = await azureDevOpsRepository.GetAllGitRepositoriesAsync(projects);
        Console.WriteLine(string.Join(",", repos.GitRepositories.Select(x => $"{x.CollectionName}:{x.ProjectName}:{x.GitRepositoryId}")));
        var commitUrls = await azureDevOpsRepository.GetFirstCommitOfAllRepositoriesAsync(userEmail, repos);
        Console.WriteLine(string.Join(", ", commitUrls));
        return "id";
    }
}