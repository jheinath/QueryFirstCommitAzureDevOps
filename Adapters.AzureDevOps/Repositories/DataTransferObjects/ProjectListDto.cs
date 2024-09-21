// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Adapters.AzureDevOps.Repositories.DataTransferObjects;

public class ProjectListDto
{
    public List<ProjectDto> Value { get; set; }
}

public class ProjectDto
{
    public string Name { get; set; }
}