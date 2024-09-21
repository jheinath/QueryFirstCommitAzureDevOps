// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace QueryFirstCommitAzureDevOps.Adapters.AzureDevOps.Repositories.DataTransferObjects.ResponseDtos;

public class RepositoryListDto
{
    public List<Repository> Value { get; set; }
}

public class Repository
{
    public string Name { get; set; }
}