namespace QueryFirstCommitAzureDevOps.Queries;

public interface IGetFirstCommitsByUserEmailQuery
{
    Task<IEnumerable<(string, DateTime)>> ExecuteAsync(string userEmail, int amountOfCommits);
}