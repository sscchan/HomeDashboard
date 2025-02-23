using System.ComponentModel.DataAnnotations;

namespace HomeDashboard.WebApplication.Dtos;

public class GetWeatherForecastsResponseDto
{
    [Required]
    public string Day { get; set; }
    
    [Required]
    public string WeatherDescription { get; set; }
    
    [Required]
    public string RainDescription { get; set; }
    
    [Required]
    public string MinimumTemperature { get; set; }
    
    [Required]
    public string MaximumTemperature { get; set; }

    public GetWeatherForecastsResponseDto(string day, string weatherDescription, string rainDescription,
        string minimumTemperature, string maximumTemperature)
    {
        Day = day;
        WeatherDescription = weatherDescription;
        RainDescription = rainDescription;
        MinimumTemperature = minimumTemperature;
        MaximumTemperature = maximumTemperature;
    }
}