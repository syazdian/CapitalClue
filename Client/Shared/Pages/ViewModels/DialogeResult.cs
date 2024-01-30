namespace CapitalClue.Frontend.Shared.Pages;

public class FetchDataResult
{    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public FetchDataResult(DateTime? startDate, DateTime? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}