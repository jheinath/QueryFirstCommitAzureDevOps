using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using QueryFirstCommitAzureDevOps.Repositories.DataTransferObjects;

namespace QueryFirstCommitAzureDevOps.Repositories;

public class AzureDevOpsRepository(IOptions<Configuration.Configuration> configuration, IHttpClientFactory httpClientFactory) 
    : IAzureDevOpsRepository
{
    private readonly string _azureDevOpsUrl = configuration.Value.AzureDevOpsUrl;
    private readonly IEnumerable<string> _collections = configuration.Value.Collections;
    private readonly string _pat = configuration.Value.PersonalAccessToken;
    
    public async Task<IDictionary<string, string>> GetAllProjectsAsync()
    {
        var result = new Dictionary<string, string>();

        foreach (var collection in _collections)
        {
            using var httpClient = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_azureDevOpsUrl}/collection/_apis/projects?api-version=6.0"),
            };
            request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_pat}"))}");
            request.Content!.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            await using var contentStream = await response.Content.ReadAsStreamAsync();
            var projects =  await JsonSerializer.DeserializeAsync<ProjectListDto>(contentStream);

            foreach (var projectName in projects!.Value.Select(x => x.Name))
            {
                result.Add(collection, projectName);
            }
        }

        return result;
    }
}