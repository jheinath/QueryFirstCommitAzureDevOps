using Application.Ports;
using Application.Queries.Interfaces;

namespace Application.Queries;

public class GetFirstCommitsByUserEmailQuery(IAzureDevOpsRepository azureDevOpsRepository)
    : IGetFirstCommitsByUserEmailQuery
{
    public async Task<IEnumerable<(string, DateTime)>> ExecuteAsync(string userEmail, int amountOfCommits)
    {
        var projects = await azureDevOpsRepository.GetAllProjectsAsync();
        var repos = await azureDevOpsRepository.GetAllGitRepositoriesAsync(projects);
        return await azureDevOpsRepository.GetFirstCommitOfAllRepositoriesAsync(userEmail, repos, amountOfCommits);
    }
}