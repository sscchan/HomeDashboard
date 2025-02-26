using HomeDashboard.Application.Entities;

namespace HomeDashboard.Application.Services;

public interface IWasteBinsCollectionDateService
{
    /// <summary>
    /// Returns the name <see cref="WasteBin"/> of the non general waste bin that is scheduled to be collected.
    /// </summary>
    /// <returns>The name <see cref="BinWaste"/> name of the next non general waste bin to be collected.</returns>
    public Task<string> GetNextNonWasteBinCollection();
}