namespace HomeDashboard.Application.Entities;

public class WasteBinCollectionDate
{
    public string BinName { get; }
    public DateOnly CollectionDate { get; }
    
    public WasteBinCollectionDate(string binName, DateOnly collectionDate)
    {
        BinName = binName;
        CollectionDate = collectionDate;
    }
}