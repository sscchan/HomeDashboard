using System.Net.Http.Json;
using HomeDashboard.Application.Entities;
using HomeDashboard.Application.Interfaces;

namespace HomeDashboard.Infrastructure.Services;

public class BOMWeatherObservationService : IWeatherObservationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    private static readonly string _bom_observation_api_url = 
        "https://api.beta.bom.gov.au/apikey/v1/observations/latest/23000/atm/surf_air?include_qc_results=false";

    public BOMWeatherObservationService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<WeatherObservation> GetWeatherObservation()
    {
        var httpClient = _httpClientFactory.CreateClient();

        var getBomObservationApiResponse = await httpClient.GetFromJsonAsync<BOMObservationApiResonse>(_bom_observation_api_url);
        return new WeatherObservation(
            DateTime.SpecifyKind(getBomObservationApiResponse.obs.datetime_utc, DateTimeKind.Utc),
            getBomObservationApiResponse.obs.temp.dry_bulb_1min_cel,
            getBomObservationApiResponse.obs.temp.apparent_1min_cel);
    }
}

// Shape of the BOM's observation API response
// See https://api.beta.bom.gov.au/apikey/v1/observations/latest/23000/atm/surf_air?include_qc_results=false
public class BOMObservationApiResonse()
{
    public BOMObservationApiResonseObs obs { get; set; }
}

public class BOMObservationApiResonseObs()
{
    public DateTime datetime_utc { get; set; }
    public BOMObservationApiResonseObsTemp temp { get; set; }
}

public class BOMObservationApiResonseObsTemp()
{
    public float dry_bulb_1min_cel { get; set; }
    public float apparent_1min_cel { get; set; }
}