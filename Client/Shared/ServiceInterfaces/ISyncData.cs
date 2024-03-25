using CapitalClue.Common.Models;

namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface ISyncData
{
    public Task SendPropertyDto(PropertyModelDto property);

    public Task SendStockDto(StockModelDto stock);
}