using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json;
using AngleSharp;
using AngleSharp.Common;
using AngleSharp.Dom;
using HomeDashboard.Application.Entities;
using HomeDashboard.Application.Interfaces;

namespace HomeDashboard.Infrastructure.Services;

public class BOMSevenDaysWeatherForecastService : IWeatherForecastService
{

    private readonly IHttpClientFactory _httpClientFactory;
    
    private static string _bom_seven_day_forecast_api_url = "https://api.beta.bom.gov.au/apikey/v1/forecasts/daily/446/201?timezone=Australia%2FAdelaide";
    private static string _bom_seven_day_forecast_text_api_url = "https://api.beta.bom.gov.au/apikey/v1/forecasts/texts?aac=SA_PW001&aac=SA_FW001&aac=SA_ME004&aac=SA_PT001&timezone=Australia%2FAdelaide";

    // BOM Icon Code to image URL map
    // See http://www.bom.gov.au/catalogue/adfdUserGuide.pdf
    private static Dictionary<int, string> BOM_ICON_CODE_TO_ICON_NAME_DICTIONARY = new ()
    {
        { 1, "sunny" },
        { 2, "clear" },
        { 3, "mostly-sunny" },
        { 4, "cloudy" },
        { 5, "missing" },
        { 6, "haze" },
        { 7, "missing" },
        { 8, "light-rain" },
        { 9, "wind" },
        { 10, "fog" },
        { 11, "showers" },
        { 12, "rain" },
        { 13, "dust" },
        { 14, "frost" },
        { 15, "snow" },
        { 16, "storms"},
        { 17, "light-showers"},
        { 18, "heavy-showers"},
        { 19, "cyclone"}
    };
    
    public BOMSevenDaysWeatherForecastService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<IList<WeatherForecast>> GetForecast()
    {
        var httpClient = _httpClientFactory.CreateClient();
        
        var getBomSevenDaysWeatherForecastApiResponse = await httpClient.GetFromJsonAsync<BOMDailyForecastTextResponseDto>(_bom_seven_day_forecast_api_url);
        var getBomSevenDaysWeatherForecastTextApiResponse = await httpClient.GetFromJsonAsync<BOMDailyForecastTextResponseDto>(_bom_seven_day_forecast_text_api_url);
        var weatherForecastDateWeatherDescriptionTextMap = getBomSevenDaysWeatherForecastTextApiResponse.fcst.daily
            .ToDictionary(
                fd => DateOnly.FromDateTime(fd.date_utc), 
                fd => fd.atm.surf_air.weather.precis_text);
        
        return getBomSevenDaysWeatherForecastApiResponse.fcst.daily.Select((df, index) =>
        {
            var dateTime = df.date_utc;
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            var weatherIconName = BOM_ICON_CODE_TO_ICON_NAME_DICTIONARY.GetValueOrDefault(df.atm.surf_air.weather.icon_code);
            var weatherDescription = weatherForecastDateWeatherDescriptionTextMap.GetValueOrDefault(DateOnly.FromDateTime(df.date_utc));

            var rainfallDistribution = new[]
            {
                df.atm.surf_air.precip.exceeding_10percentchance_total_mm,
                df.atm.surf_air.precip.exceeding_25percentchance_total_mm,
                df.atm.surf_air.precip.exceeding_50percentchance_total_mm,
                df.atm.surf_air.precip.exceeding_75percentchance_total_mm,
            };

            var rainProbabilityPercentage = df.atm.surf_air.precip.any_probability_percent;
            var minRainfall = rainfallDistribution.Min();
            var maxRainfall = rainfallDistribution.Max();
            var minimumTemperature = df.atm.surf_air.temp_min_cel;
            var maximumTemperature = df.atm.surf_air.temp_max_cel;
            
            return new WeatherForecast(dateTime, weatherIconName, weatherDescription, rainProbabilityPercentage, minRainfall, maxRainfall, minimumTemperature, maximumTemperature);
        }).ToList();
    }
}


// Dto definition for response from BOM's forecast text API
// https://api.beta.bom.gov.au/apikey/v1/forecasts/texts?aac=SA_PW001&aac=SA_FW001&aac=SA_ME004&aac=SA_PT001&timezone=Australia%2FAdelaide
public class BOMDailyForecastTextResponseDto
{
    public BOMDailyForecastResponseMetaDataDto meta { get; set; }
    public BOMDailyForecastResponseForecastDataDto fcst  { get; set; }
}

public class BOMDailyForecastResponseMetaDataDto
{
    public DateTime issue_time_utc { get; set; }
}

public class BOMDailyForecastResponseForecastDataDto
{
    public IList<BOMDailyForecastResponseForecastDataDailyEntryDto> daily { get; set; }
}

public class BOMDailyForecastResponseForecastDataDailyEntryDto
{
    public DateTime date_utc { get; set; }
    public BOMDailyForecastResponseForecastDataDailyEntryAtmosphereDto atm { get; set; }
}

public class BOMDailyForecastResponseForecastDataDailyEntryAtmosphereDto
{
    public BOMDailyForecastResponseForecastDataDailyEntryAtmosphereSurfaceAirDto surf_air { get; set; }
}

public class BOMDailyForecastResponseForecastDataDailyEntryAtmosphereSurfaceAirDto
{
    public float temp_max_cel { get; set; }
    public float temp_min_cel { get; set; }
    public BOMDailyForecastResponseForecastDataDailyEntryAtmosphereSurfaceAirPreciprDto precip { get; set; }
    public BOMDailyForecastResponseForecastDataDailyEntryAtmosphereSurfaceAirWeatherDto weather { get; set; }
}

public class BOMDailyForecastResponseForecastDataDailyEntryAtmosphereSurfaceAirPreciprDto
{
    public float exceeding_10percentchance_total_mm { get; set; }
    public float exceeding_25percentchance_total_mm { get; set; }
    public float exceeding_50percentchance_total_mm { get; set; }
    public float exceeding_75percentchance_total_mm { get; set; }
    public float any_probability_percent { get; set; }
}

public class BOMDailyForecastResponseForecastDataDailyEntryAtmosphereSurfaceAirWeatherDto
{
    public string precis_text { get; set; }
    
    public int icon_code { get; set; }
}

