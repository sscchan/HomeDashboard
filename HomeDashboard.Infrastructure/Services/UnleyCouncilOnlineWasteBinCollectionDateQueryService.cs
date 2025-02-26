using System.Globalization;
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
            
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Australia/Adelaide");
            
            var nextGeneralWasteCollectionDateString= document.Body
                .QuerySelector("div.waste-services-result.general-waste")
                .QuerySelector("div.next-service").TextContent.Trim();
            var nextGeneralWasteCollectionDateTime = DateTime.ParseExact(
                nextGeneralWasteCollectionDateString, "ddd d/M/yyyy", CultureInfo.InvariantCulture);
            var nextGeneralWasteCollectionDateTimeUtc =
                TimeZoneInfo.ConvertTimeToUtc(nextGeneralWasteCollectionDateTime, timeZoneInfo);
            
            var nextOrganicWasteCollectionDateString = document.Body
                .QuerySelector("div.waste-services-result.green-waste")
                .QuerySelector("div.next-service").TextContent.Trim();
            var nextOrganicWasteCollectionDateTime = DateTime.ParseExact(
                nextOrganicWasteCollectionDateString, "ddd d/M/yyyy", CultureInfo.InvariantCulture);
            var nextOrganicWasteCollectionDateTimeUtc =
                TimeZoneInfo.ConvertTimeToUtc(nextOrganicWasteCollectionDateTime, timeZoneInfo);
            
            var nextRecyclingCollectionDateString = document.Body
                .QuerySelector("div.waste-services-result.recycling")
                .QuerySelector("div.next-service").TextContent.Trim();
            var nextRecyclingCollectionDateTime = DateTime.ParseExact(
                nextRecyclingCollectionDateString, "ddd d/M/yyyy", CultureInfo.InvariantCulture);
            var nextRecyclingCollectionDateTimeUtc =
                TimeZoneInfo.ConvertTimeToUtc(nextRecyclingCollectionDateTime, timeZoneInfo);
            
            return new List<WasteBinCollectionDate>()
            {
                new WasteBinCollectionDate(WasteBin.BLUE_GENERAL_WASTE, nextGeneralWasteCollectionDateTimeUtc),
                new WasteBinCollectionDate(WasteBin.GREEN_ORGANIC_WASTE, nextOrganicWasteCollectionDateTimeUtc),
                new WasteBinCollectionDate(WasteBin.YELLOW_RECYCLE, nextRecyclingCollectionDateTime)
            };
        }

        throw new ApplicationException("Failed to get waste bin collection dates from Unley Council API service.");

    }
}