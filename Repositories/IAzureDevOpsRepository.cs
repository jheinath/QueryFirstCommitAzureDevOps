namespace QueryFirstCommitAzureDevOps.Repositories;

public interface IAzureDevOpsRepository
{
    Task<IDictionary<string, string>> GetAllProjectsAsync();
}