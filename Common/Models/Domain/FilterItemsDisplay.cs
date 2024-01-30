namespace CapitalClue.Common.Models.Domain;

public class FilterItemsDisplay
{
    public FilterItemsDisplay()
    {
        LoBs = new List<LoB>();
        StoreNumbers = new List<string>();
        Brands = new List<string>();
        RebateTypes = new List<string>();
    }

    public List<LoB> LoBs { get; set; }
    public List<string> StoreNumbers { get; set; }
    public List<string> Brands { get; set; }
    public List<string> RebateTypes { get; set; }
}