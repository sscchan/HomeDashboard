namespace HomeDashboard.Application.Entities;

public class WeatherObservation
{
    public DateTime ObservationDate { get; private set; }
    public float DryBulbTemperature { get; private set; }
    public float ApparentTemperature { get; private set; }
    
    public WeatherObservation(DateTime observationDate, float dryBulbTemperature, float apparentTemperature)
    {
        ObservationDate = observationDate;
        DryBulbTemperature = dryBulbTemperature;
        ApparentTemperature = apparentTemperature;
    }
}