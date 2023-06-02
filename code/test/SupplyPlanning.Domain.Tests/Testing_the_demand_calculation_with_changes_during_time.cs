using FluentAssertions;
using SupplyPlanning.Domain.Core;
using SupplyPlanning.Domain.SupplyPlans;
using SupplyPlanning.Domain.Tests.TestUtils;
using static System.DayOfWeek;
using static SupplyPlanning.Domain.Tests.TestUtils.TestData.TestProducts;
namespace SupplyPlanning.Domain.Tests;

public class Testing_the_demand_calculation_with_changes_during_time : TimeTravelingTest
{
    [Fact]
    public void calculate_total_demands_of_period()
    {
        Clock.TravelTo("2023-01-01");
        var demands = new List<DemandItem>() {
            new(50, "KG", Apple),
            new(30, "KG", Banana)
        };
        var plan = Schedule.SupplyPlan()
            .UsingTheClock(Clock)
            .BetweenDates(new(2023, 01, 01), new(2023, 12, 31))
            .Named("Weekly Fruit Supply")
            .OnDays(Monday, Wednesday, Friday)
            .IncludingDemands(demands)
            .Build();

        Clock.TravelTo("2023-02-01");
        var newDays = new List<DayOfWeek>() { Tuesday, Thursday };
        var newDemands = new List<DemandItem>() {
            new(30, "KG", Apple),
            new(40, "KG", Orange),
        };
        plan.ChangePlan(newDays, newDemands, Clock);

        Clock.TravelTo("2023-02-15");
        var calculationRange = new DateRange(new(2023, 01, 20), new(2023, 02, 10)); //(8 active days)
        var expectedTotal = new List<DemandItem>()
        {
            new(340, "KG", Apple),
            new(150, "KG", Banana),
            new(120, "KG", Orange),
        };

        var actualTotal = plan.TotalDemandsInRange(calculationRange);

        actualTotal.Should().BeEquivalentTo(expectedTotal);
    }

    [Fact]
    public void calculate_demands_of_days_in_a_period()
    {
        Clock.TravelTo("2023-01-01");
        var demands = new List<DemandItem>() {
            new(50, "KG", Apple),
            new(30, "KG", Banana)
        };
        var plan = Schedule.SupplyPlan()
            .UsingTheClock(Clock)
            .BetweenDates(new(2023, 01, 01), new(2023, 12, 31))
            .Named("Weekly Fruit Supply")
            .OnDays(Monday, Wednesday, Friday)
            .IncludingDemands(demands)
            .Build();

        Clock.TravelTo("2023-02-01");
        var newDays = new List<DayOfWeek>() { Tuesday, Thursday };
        var newDemands = new List<DemandItem>() {
            new(4, "KG", Orange),
        };
        plan.ChangePlan(newDays, newDemands, Clock);


        Clock.TravelTo("2023-02-15");
        var calculationRange = new DateRange(new(2023, 01, 20), new(2023, 02, 10)); //(8 active days)
        var expectedDemands = new List<DemandsInDay>()
        {
            new(new(2023,01,20), demands),
            new(new(2023,01,23), demands),
            new(new(2023,01,25), demands),
            new(new(2023,01,27), demands),
            new(new(2023,01,30), demands),

            new(new(2023,02,2), newDemands),
            new(new(2023,02,7), newDemands),
            new(new(2023,02,9), newDemands)
        };

        var actualDemands = plan.DemandsInRange(calculationRange);

        //actualDemands.Should().BeEquivalentTo(expectedDemands);
    }
}