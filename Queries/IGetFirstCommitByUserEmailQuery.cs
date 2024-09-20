namespace QueryFirstCommitAzureDevOps.Queries;

public interface IGetFirstCommitByUserEmailQuery
{
    Task<string> ExecuteAsync(string userEmail, bool onlyMasterOrMain);
}