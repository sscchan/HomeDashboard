using System.ComponentModel.DataAnnotations;

namespace HomeDashboard.WebApplication.Dtos;

public class GetWeatherObservationResponseDto
{
    [Required]
    public DateTime ObservationTakenAt { get; set; }
    
    [Required]
    public float DryBulbTemperature { get; set; }
    
    [Required]
    public float ApparentTemperature { get; set; }
    
    public GetWeatherObservationResponseDto(DateTime observationTakenAt, float dryBulbTemperature, float apparentTemperature)
    {
        ObservationTakenAt = observationTakenAt;
        DryBulbTemperature = dryBulbTemperature;
        ApparentTemperature = apparentTemperature;
    }
}