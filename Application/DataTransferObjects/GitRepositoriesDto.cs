namespace QueryFirstCommitAzureDevOps.Adapters.AzureDevOps.Repositories.DataTransferObjects;

public class GitRepositoriesDto
{
    public IEnumerable<GitRepositoryDto> GitRepositories { get; set; }
    
    public class GitRepositoryDto
    {
        public string CollectionName { get; set; }
        public string ProjectName { get; set; }
        public string GitRepositoryId { get; set; }
    }
}

