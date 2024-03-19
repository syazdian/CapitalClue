namespace CapitalClue.Common.Models.Domain;

public class FilterItemsDisplay
{
    public FilterItemsDisplay()
    {
        StockFilterDisplayObj = new StockFilterDisplay();
        PropertyFilterObj = new PropertyFilterDisplay();
    }

    public StockFilterDisplay StockFilterDisplayObj { get; set; }
    public PropertyFilterDisplay PropertyFilterObj { get; set; }

    public class StockFilterDisplay
    {
        public StockFilterDisplay()
        {
            Currencies = new List<string>();
            Stocks = new List<string>();
        }

        public List<string> Currencies { get; set; }
        public List<string> Stocks { get; set; }
    }

    public class PropertyFilterDisplay
    {
        public PropertyFilterDisplay()
        {
            Cities = new List<string>();
            PropertyType = new List<string>();
        }

        public List<string> Cities { get; set; }
        public List<string> PropertyType { get; set; }
    }
}