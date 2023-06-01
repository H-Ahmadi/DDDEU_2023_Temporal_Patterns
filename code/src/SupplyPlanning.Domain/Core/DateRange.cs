namespace SupplyPlanning.Domain.Core;

public class DateRange
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public DateRange(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
    public bool Contains(DateTime dateTime)
    {
        return StartDate <= dateTime && EndDate >= dateTime;
    }
    public IEnumerable<DateTime> Days()
    {
        for (var date = StartDate; date <= EndDate; date = date.AddDays(1))
            yield return date.Date;
    }
}