// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace QueryFirstCommitAzureDevOps.Adapters.AzureDevOps.Repositories.DataTransferObjects.ResponseDtos;

public class ProjectListDto
{
    public List<ProjectDto> Value { get; set; }
}

public class ProjectDto
{
    public string Name { get; set; }
}