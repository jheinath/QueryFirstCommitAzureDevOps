namespace QueryFirstCommitAzureDevOps.Repositories.DataTransferObjects;

public class RepositoryListDto
{
    public List<Repository> Value { get; set; }
}

public class Repository
{
    public string Name { get; set; }
}