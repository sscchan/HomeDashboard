namespace HomeDashboard.Application.Entities;

public class WeatherForecast
{
    public string Day { get; private set; }
    public string WeatherDescription { get; private set; }
    public string RainDescription { get; private set; }
    public string MinimumTemperature { get; private set; }
    public string MaximumTemperature { get; private set; }

    public WeatherForecast(string day, string weatherDescription, string rainDescription, string minimumTemperature, string maximumTemperature)
    {
        Day = day;
        WeatherDescription = weatherDescription;
        RainDescription = rainDescription;
        MinimumTemperature = minimumTemperature;
        MaximumTemperature = maximumTemperature;
    }
}