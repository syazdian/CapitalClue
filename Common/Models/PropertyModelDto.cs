
namespace CapitalClue.Common.Models;

public class PropertyModelDto
{
    public PropertyModelDto()
    {
        PropertyValueIndices = new();
    }

    public string City { get; set; } = string.Empty;
    public string PropertyType { get; set; } = string.Empty;
    public List<PropertyValueIndex> PropertyValueIndices { get; set; }
}

public class PropertyValueIndex
{
    public DateTime DateTime { get; set; } = DateTime.MinValue;
    public double Value { get; set; } = 0;
}
