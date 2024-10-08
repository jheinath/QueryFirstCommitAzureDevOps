﻿using QueryFirstCommitAzureDevOps.Adapters.AzureDevOps.Repositories.DataTransferObjects;

namespace Application.Ports;

public interface IAzureDevOpsRepository
{
    Task<ProjectsDto> GetAllProjectsAsync();
    Task<GitRepositoriesDto> GetAllGitRepositoriesAsync(ProjectsDto projectsDto);
    Task<IEnumerable<(string, DateTime)>> GetFirstCommitOfAllRepositoriesAsync(string userEmail, GitRepositoriesDto repositoriesDto, int amountOfCommits);
}