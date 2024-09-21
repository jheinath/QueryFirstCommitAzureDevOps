using System.ComponentModel.DataAnnotations;

namespace Adapters.AzureDevOps;

public class Configuration
{
    [Required(ErrorMessage = "AzureDevOpsUrl is required")]
    [Url(ErrorMessage = "AzureDevOpsUrl must be a valid URL")]
    public string AzureDevOpsUrl { get; set; }
    
    [Required(ErrorMessage = "Collections are required")]
    public IEnumerable<string> Collections { get; set; }
    
    [Required(ErrorMessage = "PersonalAccessToken is required")]
    public string PersonalAccessToken { get; set; }
}
