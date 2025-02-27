using HomeDashboard.Application.Entities;

namespace HomeDashboard.Application.Interfaces;

public interface IWeatherObservationService
{
    public Task<WeatherObservation> GetWeatherObservation();
}