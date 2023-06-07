using FluentAssertions;
using SupplyPlanning.Domain.Core;
using SupplyPlanning.Domain.SupplyPlans;
using SupplyPlanning.Domain.Tests.TestUtils;
using static System.DayOfWeek;
using static SupplyPlanning.Domain.Tests.TestUtils.TestData.TestProducts;

namespace SupplyPlanning.Domain.Tests;

public class Testing_the_demand_calculation
{
    [Fact]
    public void calculate_total_demands_in_a_period()
    {
        var plan = Schedule.SupplyPlan()
                        .BetweenDates(new(2023, 01, 01), new(2023, 12, 31))
                        .Named("Weekly Fruit Supply")
                        .OnDays(Monday, Wednesday, Friday)
                        .IncludingDemand(new(50, "KG", Apple))
                        .IncludingDemand(new(30, "KG", Banana))
                        .IncludingDemand(new(20, "KG", Orange))
                        .Build();

        var calculationRange = new DateRange(new(2023, 01, 15), new(2023, 02, 01)); //(8 active days)
        var expectedDemands = new List<DemandItem>()
        {
            new(400, "KG", Apple),
            new(240, "KG", Banana),
            new(160, "KG", Orange),
        };

        var demands = plan.TotalDemandsInRange(calculationRange);

        demands.Should().BeEquivalentTo(expectedDemands);
    }

    [Fact]
    public void calculate_demands_of_days_in_a_period()
    {
        var demands = new List<DemandItem>() {
            new(50, "KG", Apple), 
            new(30, "KG", Banana),
            new(20, "KG", Orange)
        };
        var plan = Schedule.SupplyPlan()
                            .BetweenDates(new(2023, 01, 01), new(2023, 12, 31))
                            .Named("Weekly Fruit Supply")
                            .OnDays(Monday, Wednesday, Friday)
                            .IncludingDemands(demands)
                            .Build();

        var calculationRange = new DateRange(new(2023, 01, 15), new(2023, 01, 25)); //(5 active days)
        var expectedDemands = new List<DemandsInDay>()
        {
            new(new(2023,01,16), demands),
            new(new(2023,01,18), demands),
            new(new(2023,01,20), demands),
            new(new(2023,01,23), demands),
            new(new(2023,01,25), demands)
        };

        var actualDemands = plan.DemandsInRange(calculationRange);

        actualDemands.Should().BeEquivalentTo(expectedDemands);
    }
}