using HomeDashboard.Application.Services;
using HomeDashboard.WebApplication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeDashboard.WebApplication.Controllers;

[ApiController]
[Route("api/WasteBins")]
public class WasteBinController
{
    private IWasteBinsCollectionDateService _wasteBinCollectionDateService;

    public WasteBinController(IWasteBinsCollectionDateService wasteBinCollectionDateService)
    {
        _wasteBinCollectionDateService = wasteBinCollectionDateService;
    }
    
    /// <summary>
    /// Retrieves information related to Waste Bins
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IList<GetWasteBinResponseDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<IList<GetWasteBinResponseDto>> Index()
    {
        var nextWasteBinCollectionDates = await _wasteBinCollectionDateService.GetNextWasteBinCollectionDates();

        return nextWasteBinCollectionDates
            .Select(nwbcd => new GetWasteBinResponseDto(nwbcd.BinName, nwbcd.CollectionDate))
            .ToList();
    }
}