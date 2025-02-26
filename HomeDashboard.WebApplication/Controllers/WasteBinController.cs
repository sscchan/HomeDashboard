using HomeDashboard.Application.Services;
using HomeDashboard.WebApplication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeDashboard.WebApplication.Controllers;

[ApiController]
[Route("api/WasteBins")]
public class WasteBinController
{
    private readonly IWasteBinsCollectionDateService _wasteBinCollectionDateService;

    public WasteBinController(IWasteBinsCollectionDateService wasteBinCollectionDateService)
    {
        _wasteBinCollectionDateService = wasteBinCollectionDateService;
    }
    
    /// <summary>
    /// Retrieves the name of the next non-general waste bin to be collected.
    /// </summary>
    [HttpGet]
    [Route("next")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(500)]
    public async Task<string> GetNextNonWasteBinCollection()
    {
        return await _wasteBinCollectionDateService.GetNextNonWasteBinCollection();
    }
}