namespace QueryFirstCommitAzureDevOps.Repositories.DataTransferObjects;

public class ProjectListDto
{
    public int Count { get; set; }
    public List<ProjectDto> Value { get; set; }
}

public class ProjectDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public ProjectState State { get; set; }
    public int Revision { get; set; }
    public ProjectVisibility Visibility { get; set; }
    public DateTime LastUpdateTime { get; set; }
}

public enum ProjectState
{
    WellFormed,
    Creating,
    Deleting,
    Deleted
}

public enum ProjectVisibility
{
    Private,
    Public
}