using System.ComponentModel.DataAnnotations;

namespace QueryFirstCommitAzureDevOps.Configuration;

public class Configuration
{
    [Required(ErrorMessage = "AzureDevOpsUrl is required")]
    [Url(ErrorMessage = "AzureDevOpsUrl must be a valid URL")]
    public string AzureDevOpsUrl { get; set; }
    public IEnumerable<string> Collections { get; set; }
    public string PersonalAccessToken { get; set; }
}
