namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface ICsvExport
{
    public Stream ToCsv<T>(List<T> list);
}