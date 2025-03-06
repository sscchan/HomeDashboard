using System.Globalization;
using HomeDashboard.WebApplication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeDashboard.WebApplication.Controllers;

[ApiController]
[Route("api/Status")]
public class StatusController
{
    private static DateTime DEPLOYMENT_DATETIME = DateTime.UtcNow;
    
    /// <summary>
    /// Retrieves version of the backend server. Intended to be used by web clients to determine if they need to reload
    /// the dashboard entirely.
    /// </summary>
    [HttpGet]
    [Route("Version")]
    [ProducesResponseType(typeof(GetStatusVersionResponseDto), 200)]
    [ProducesResponseType(500)]
    public async Task<GetStatusVersionResponseDto> GetVersion()
    {
        return new GetStatusVersionResponseDto(DEPLOYMENT_DATETIME.ToString(CultureInfo.InvariantCulture), DEPLOYMENT_DATETIME);
    }
}