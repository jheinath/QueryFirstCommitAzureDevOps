namespace QueryFirstCommitAzureDevOps.Queries;

public interface IGetFirstCommitByUsernameQuery
{
    Task<string> ExecuteAsync(string username, bool onlyMasterOrMain);
}