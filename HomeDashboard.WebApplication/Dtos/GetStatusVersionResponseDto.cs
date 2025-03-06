using System.ComponentModel.DataAnnotations;

namespace HomeDashboard.WebApplication.Dtos;

public class GetStatusVersionResponseDto
{
    [Required]
    public string Version { get; set; }
    
    [Required]
    public DateTime DeploymentDateTime  { get; set; }

    public GetStatusVersionResponseDto(string version, DateTime deploymentDateTime)
    {
        Version = version;
        DeploymentDateTime = deploymentDateTime;
    }
}