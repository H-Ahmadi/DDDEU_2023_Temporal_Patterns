namespace SupplyPlanning.Domain.Core;

public interface IClock
{
    public DateTime Now();
}

public class SystemClock : IClock
{
    public static IClock Instance = new SystemClock();
    private SystemClock() { }
    public DateTime Now() => DateTime.Now;
}