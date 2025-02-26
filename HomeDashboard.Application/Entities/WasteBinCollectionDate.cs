namespace HomeDashboard.Application.Entities;

public class WasteBinCollectionDate
{
    public string BinName { get; private set; }
    public DateTime CollectionDate { get; private set; }
    
    public WasteBinCollectionDate(string binName, DateTime collectionDate)
    {
        BinName = binName;
        CollectionDate = collectionDate;
    }
}