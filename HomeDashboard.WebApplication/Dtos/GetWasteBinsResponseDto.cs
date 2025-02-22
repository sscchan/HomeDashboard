using System.ComponentModel.DataAnnotations;

namespace HomeDashboard.WebApplication.Dtos;

public class GetWasteBinResponseDto
{
    [Required]
    public string BinName { get; }
    
    [Required]
    public DateOnly CollectionDate { get; }

    public GetWasteBinResponseDto(string binName, DateOnly collectionDate)
    {
        BinName = binName;
        CollectionDate = collectionDate;
    }
}