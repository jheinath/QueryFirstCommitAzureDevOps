namespace QueryFirstCommitAzureDevOps.Repositories;

public interface IAzureDevOpsRepository
{
    Task<ProjectsDto> GetAllProjectsAsync();
    Task<GitRepositoriesDto> GetAllGitRepositories(ProjectsDto projectsDto);
}