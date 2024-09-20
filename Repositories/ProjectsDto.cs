namespace QueryFirstCommitAzureDevOps.Repositories;

public class ProjectsDto
{
    public IEnumerable<ProjectDto> Projects { get; set; }
    
    public class ProjectDto
    {
        public string CollectionName { get; set; }
        public string ProjectName { get; set; }
    }
}

