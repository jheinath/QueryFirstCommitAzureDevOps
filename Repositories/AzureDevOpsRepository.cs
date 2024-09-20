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
            foreach (var projectName in projects!.Value.Select(x => x.Name))
            {
                result.Projects = result.Projects.Append(new ProjectsDto.ProjectDto
                {
                    CollectionName = collection,
                    ProjectName = projectName
                });
            }
        }

        return result;
    }

    private static void EnhanceResult(ProjectListDto? projects, ProjectsDto result, string collection)
    {
        
    }

    public async Task<GitRepositoriesDto> GetAllGitRepositoriesAsync(ProjectsDto projectsDto)
    {
        var result = new GitRepositoriesDto
        {
            GitRepositories = new List<GitRepositoriesDto.GitRepositoryDto>()
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
            foreach (var repository in repositories!.Value)
            {
                result.GitRepositories = result.GitRepositories.Append(new GitRepositoriesDto.GitRepositoryDto
                {
                    ProjectName = project.ProjectName,
                    CollectionName = project.CollectionName,
                    GitRepositoryId = repository.Name
                });
            }
            
        }

        return result;
    }

    public async Task<IEnumerable<string>> GetFirstCommitOfAllRepositoriesAsync(string userEmail, GitRepositoriesDto repositoriesDto)
    {
        var commits = new List<CommitDto>();
        foreach (var gitRepository in repositoriesDto.GitRepositories)
        {
            using var httpClient = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_azureDevOpsUrl}/{gitRepository.CollectionName}/{gitRepository.ProjectName}/_apis/git/repositories/{gitRepository.GitRepositoryId}/commits?searchCriteria.author={userEmail}&top=100&api-version=6.0")
            };
            request.Headers.Add("Authorization",
                $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_pat}"))}");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var commitsDto = JsonConvert.DeserializeObject<CommitsDto>(jsonResponse);

            if (commitsDto != null) commits = commits.Append(commitsDto.Value.MinBy(x => x.Author.Date)).ToList()!;
        }

        var result = commits.Where(dto => dto?.Author?.Date is not null).MinBy(x => x.Author.Date)?.RemoteUrl;
        return [result ?? string.Empty];
    }
}