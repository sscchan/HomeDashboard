using HomeDashboard.Application.Entities;
using HomeDashboard.Application.Services;

namespace HomeDashboard.Application.Interfaces;

public interface IWasteBinCollectionDateQueryService
{
    public Task<IList<WasteBinCollectionDate>> GetNextWasteBinCollectionDates();
}