using HomeDashboard.Application.Entities;
using HomeDashboard.Application.Interfaces;

namespace HomeDashboard.Application.Services;

public class WasteBinsCollectionDateService : IWasteBinsCollectionDateService
{
    
    private IWasteBinCollectionDateQueryService _wasteBinCollectionDateQueryService;

    public WasteBinsCollectionDateService(IWasteBinCollectionDateQueryService wasteBinCollectionDateQueryService)
    {
        _wasteBinCollectionDateQueryService = wasteBinCollectionDateQueryService;
    }
    
    public async Task<IList<WasteBinCollectionDate>> GetNextWasteBinCollectionDates()
    {
        return await _wasteBinCollectionDateQueryService.GetNextWasteBinCollectionDates();
    }
}