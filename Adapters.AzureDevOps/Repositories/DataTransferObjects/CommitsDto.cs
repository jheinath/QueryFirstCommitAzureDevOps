// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace QueryFirstCommitAzureDevOps.Adapters.AzureDevOps.Repositories.DataTransferObjects.ResponseDtos;

public class CommitsDto
{
    public int Count { get; set; }
    public List<CommitDto> Value { get; set; }
}

public class CommitDto
{
    public AuthorDto Author { get; set; }
    public string RemoteUrl { get; set; }
}

public class AuthorDto
{
    public DateTime Date { get; set; }
}