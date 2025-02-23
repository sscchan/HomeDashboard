using HomeDashboard.Application.Entities;

namespace HomeDashboard.Application.Interfaces;

public interface IWeatherForecastService
{
    public Task<IList<WeatherForecast>> GetForecast();
}