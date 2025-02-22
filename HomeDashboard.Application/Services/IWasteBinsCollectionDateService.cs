using HomeDashboard.Application.Entities;

namespace HomeDashboard.Application.Services;

public interface IWasteBinsCollectionDateService
{
    public Task<IList<WasteBinCollectionDate>> GetNextWasteBinCollectionDates();
}