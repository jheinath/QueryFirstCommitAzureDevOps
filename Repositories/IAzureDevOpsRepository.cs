namespace QueryFirstCommitAzureDevOps.Repositories;

public interface IAzureDevOpsRepository
{
    Task<ProjectsDto> GetAllProjectsAsync();
    Task<GitRepositoriesDto> GetAllGitRepositoriesAsync(ProjectsDto projectsDto);
    Task<IEnumerable<string>> GetFirstCommitOfAllRepositoriesAsync(string userEmail, GitRepositoriesDto repositoriesDto);
}