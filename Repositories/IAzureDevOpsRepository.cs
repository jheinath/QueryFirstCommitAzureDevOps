namespace QueryFirstCommitAzureDevOps.Repositories;

public interface IAzureDevOpsRepository
{
    Task<IEnumerable<Tuple<string, string>>> GetAllProjectsAsync();
}