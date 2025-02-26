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
    public async Task<string> GetNextNonWasteBinCollection()
    {
        var binCollectionDates =  await _wasteBinCollectionDateQueryService.GetNextWasteBinCollectionDates();
        //TODO: Refactor to use TimeProvider
        var nextNonGeneralWasteBinCollected = binCollectionDates
            .Where(bcd => bcd.BinName != WasteBin.BLUE_GENERAL_WASTE)
            .MinBy(bcd => bcd.CollectionDate)
            .BinName;

        return nextNonGeneralWasteBinCollected;
    }
}