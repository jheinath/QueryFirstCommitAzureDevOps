using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QueryFirstCommitAzureDevOps.Repositories.DataTransferObjects;

namespace QueryFirstCommitAzureDevOps.Repositories;

public class AzureDevOpsRepository(IOptions<Configuration.Configuration> configuration, IHttpClientFactory httpClientFactory) 
    : IAzureDevOpsRepository
{
    private readonly string _azureDevOpsUrl = configuration.Value.AzureDevOpsUrl;
    private readonly IEnumerable<string> _collections = configuration.Value.Collections;
    private readonly string _pat = configuration.Value.PersonalAccessToken;
    
    public async Task<ProjectsDto> GetAllProjectsAsync()
    {
        var result = new ProjectsDto
        {
            Projects = new List<ProjectsDto.ProjectDto>()
        };
        
        foreach (var collection in _collections)
        {
            using var httpClient = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_azureDevOpsUrl}/{collection}/_apis/projects?api-version=6.0"),
            };
            request.Headers.Add("Authorization",
                $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_pat}"))}");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var projects = JsonConvert.DeserializeObject<ProjectListDto>(jsonResponse);
            EnhanceResult(projects, result, collection);
        }

        return result;
    }

    private static void EnhanceResult(ProjectListDto? projects, ProjectsDto result, string collection)
    {
        foreach (var projectName in projects!.Value.Select(x => x.Name))
        {
            result.Projects.ToList().Add(new ProjectsDto.ProjectDto
            {
                CollectionName = collection,
                ProjectName = projectName
            });
        }
    }

    public async Task<GitRepositoriesDto> GetAllGitRepositories(ProjectsDto projectsDto)
    {
        var result = new GitRepositoriesDto
        {
            Projects = projectsDto.Projects.Select(x => new GitRepositoriesDto.ProjectDto
            {
                CollectionName = x.CollectionName,
                ProjectName = x.ProjectName,
                GitRepositoryIds = new List<string>()
            })
        };
        
        foreach (var project in projectsDto.Projects)
        {
            using var httpClient = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_azureDevOpsUrl}/{project.CollectionName}/{project.ProjectName}/_apis/git/repositories?api-version=6.0"),
            };
            request.Headers.Add("Authorization",
                $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_pat}"))}");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var repositories = JsonConvert.DeserializeObject<RepositoryListDto>(jsonResponse);
            EnhanceResult(repositories, result, project);
        }

        return result;
    }
    
    private static void EnhanceResult(RepositoryListDto? repositoryListDto, GitRepositoriesDto result, ProjectsDto.ProjectDto projectsDto)
    {
        foreach (var resultProject in result.Projects.Where(x => x.ProjectName == projectsDto.ProjectName))
        {
            resultProject.GitRepositoryIds = repositoryListDto!.Value.Select(x => x.Name);
        }
    }
}