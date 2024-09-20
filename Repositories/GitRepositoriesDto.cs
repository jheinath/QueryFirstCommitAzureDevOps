namespace QueryFirstCommitAzureDevOps.Repositories;

public class GitRepositoriesDto
{
    public IEnumerable<ProjectDto> Projects { get; set; }
    
    public class ProjectDto
    {
        public string CollectionName { get; set; }
        public string ProjectName { get; set; }
        public IEnumerable<string> GitRepositoryIds { get; set; }
    }
}

