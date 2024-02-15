
namespace CapitalClue.Common.Models;

public class StockModelDto
{
    public StockModelDto()
    {
        StockValueIndices = new();
    }

    public string StockName { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public List<StockValueIndex> StockValueIndices { get; set; }
}

public class StockValueIndex
{
    public DateTime DateTime { get; set; } = DateTime.MinValue;
    public double Value { get; set; } = 0;
}
