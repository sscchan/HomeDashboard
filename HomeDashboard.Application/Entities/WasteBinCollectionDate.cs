namespace HomeDashboard.Application.Entities;

public class WasteBinCollectionDate
{
    public string BinName { get; private set; }
    public DateOnly CollectionDate { get; private set; }
    
    public WasteBinCollectionDate(string binName, DateOnly collectionDate)
    {
        BinName = binName;
        CollectionDate = collectionDate;
    }
}