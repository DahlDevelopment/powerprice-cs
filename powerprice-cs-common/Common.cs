namespace powerprice_cs_common;



public static class DocumentTypes
{
    public static readonly string A44 = "A44";  // Price Document
}

public static class Zones
{
    public static readonly string NO1 = "10YNO-1--------2";  // NO1 Eastern Norway
    public static readonly string NO2 = "10YNO-2--------T";  // NO2 Southern Norway
    public static readonly string NO3 = "10YNO-3--------J";  // NO3 Central Norway
    public static readonly string NO4 = "10YNO-4--------9";  // NO4 Northern Norway
    public static readonly string NO5 = "10Y1001A1001A48H";  // NO5 Western Norway
}

public struct PriceDataOptions
{
    public string DocumentType { set; get; } = DocumentTypes.A44;
    public string Zone { set; get; }
    public DateOnly Date { set; get; }

    

    public PriceDataOptions(DateOnly date, string zone, string documentType)
    {
        Zone = zone;
        Date = date;
        DocumentType = documentType;
    }
    public PriceDataOptions(DateOnly date, string zone) : this(date, zone, DocumentTypes.A44)
    {
    }

}