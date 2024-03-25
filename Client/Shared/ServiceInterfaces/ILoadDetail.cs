namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface ILoadDetail
{
    public Task<string> GetDeatilByPhoneNumber(string phone);

    public Task<string> GetDeatilByImei(string imei);

    public Task<string> GetDeatilByOrderNumber(string ordernumber);
}