namespace QueryFirstCommitAzureDevOps.Repositories.DataTransferObjects;

public class CommitsDto
{
    public int Count { get; set; }
    public List<CommitDto> Value { get; set; }
}

public class CommitDto
{
    public string CommitId { get; set; }
    public AuthorDto Author { get; set; }
    public CommitterDto Committer { get; set; }
    public string Comment { get; set; }
    public string RemoteUrl { get; set; } // Assuming this might be included
    public string PushId { get; set; } // If push details are available
    public DateTime PushDate { get; set; } // If push date is available
    public List<ChangeDto> Changes { get; set; } // If changes are part of the response
}

public class AuthorDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }
    
}

public class CommitterDto
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class ChangeDto
{
    public string ChangeType { get; set; }
    public ItemDto Item { get; set; }
}

public class ItemDto
{
    public string Path { get; set; }
    public string Url { get; set; }
}