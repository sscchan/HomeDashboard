using System.Net.Http.Json;
using AngleSharp.Html.Parser;
using HomeDashboard.Application.Entities;
using HomeDashboard.Application.Interfaces;
using HomeDashboard.Infrastructure.Dtos;

namespace HomeDashboard.Infrastructure.Services;

public class UnleyCouncilOnlineWasteBinCollectionDateQueryService : IWasteBinCollectionDateQueryService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UnleyCouncilOnlineWasteBinCollectionDateQueryService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<IList<WasteBinCollectionDate>> GetNextWasteBinCollectionDates()
    {
        var httpClient = _httpClientFactory.CreateClient();
        var wasteServiceQueryUri =
            "https://www.unley.sa.gov.au/ocapi/Public/myarea/wasteservices?geolocationid=3710c756-7f63-4f09-a53a-e8a9473ce03e&ocsvclang=en-AU&pageLink=/$b9015858-988c-48a4-9473-7c193df083e4$/Bins-pets-parking/Waste-recycling/Rubbish-collection-dates";
        var getUnleyCouncilWasteBinCollectionDateResponse = await httpClient.GetFromJsonAsync<UnleyCouncilWasteServiceResponseDto>(wasteServiceQueryUri);

        if (getUnleyCouncilWasteBinCollectionDateResponse != null && getUnleyCouncilWasteBinCollectionDateResponse.success)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(getUnleyCouncilWasteBinCollectionDateResponse.responseContent);
            
            var nextGeneralWasteCollectionDateString= document.Body
                .QuerySelector("div.waste-services-result.general-waste")
                .QuerySelector("div.next-service").TextContent.Trim();
            var nextGreenWasteCollectionDateString = document.Body
                .QuerySelector("div.waste-services-result.green-waste")
                .QuerySelector("div.next-service").TextContent.Trim();
            var nextRecyclingCollectionDateString = document.Body
                .QuerySelector("div.waste-services-result.recycling")
                .QuerySelector("div.next-service").TextContent.Trim();
            
            return new List<WasteBinCollectionDate>()
            {
                new WasteBinCollectionDate("General Waste (Blue)", DateOnly.ParseExact(nextGeneralWasteCollectionDateString, "ddd d/M/yyyy")),
                new WasteBinCollectionDate("Organic Waste (Green)", DateOnly.ParseExact(nextGreenWasteCollectionDateString, "ddd d/M/yyyy")),
                new WasteBinCollectionDate("Recycling (Yellow)", DateOnly.ParseExact(nextRecyclingCollectionDateString, "ddd d/M/yyyy"))
            };
        }

        throw new ApplicationException("Failed to get waste bin collection dates from Unley Council API service.");

    }
}