namespace HomeDashboard.Application.Entities;

public class WeatherForecast
{
    public DateTime DateTime { get; private set; }
    public string WeatherIconName { get; private set; }
    public string WeatherDescription { get; private set; }
    public float RainProbabilityPercentage { get; private set; }
    public float MinimumRainfall { get; private set; }
    public float MaximumRainfall { get; private set; }
    public float MinimumTemperature { get; private set; }
    public float MaximumTemperature { get; private set; }

    public WeatherForecast(DateTime dateTime, string weatherIconName, string weatherDescription, float rainProbabilityPercentage, float minimumRainfall, float maximumRainfall, float minimumTemperature, float maximumTemperature)
    {
        DateTime = dateTime;
        WeatherIconName = weatherIconName;
        WeatherDescription = weatherDescription;
        RainProbabilityPercentage = rainProbabilityPercentage;
        MinimumRainfall = minimumRainfall;
        MaximumRainfall = maximumRainfall;
        MinimumTemperature = minimumTemperature;
        MaximumTemperature = maximumTemperature;
    }
}