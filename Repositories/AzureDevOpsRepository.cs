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
    
    public async Task<IEnumerable<Tuple<string, string>>> GetAllProjectsAsync()
    {
        var result = new List<Tuple<string, string>>();

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
                result.Add(new Tuple<string, string>(collection, projectName));
            }
        }

        return result;
    }
}