using FluentAssertions;
using SupplyPlanning.Domain.Customers;
using SupplyPlanning.Domain.Tests.TestUtils;

namespace SupplyPlanning.Domain.Tests
{
    public class Testing_history_of_address_for_customer : TimeTravelingTest
    {
        [Fact]
        public void introducing_a_customer_with_an_address()
        {
            var customer = new Customer(1, "Olivia", "Johnson", "123 Maple Street");

            customer.Address.GetValue().Should().Be("123 Maple Street");
        }

        [Fact]
        public void getting_the_current_address_after_changing_the_address()
        {
            Clock.TravelThroughTime("2023-01-01");
            var customer = new Customer(1, "Olivia", "Johnson", "123 Maple Street", Clock);

            Clock.TravelThroughTime("2023-03-01");
            customer.ChangeAddress("456 Oak Avenue", Clock);

            var actualAddress = customer.Address.GetValue();
            actualAddress.Should().Be("456 Oak Avenue");
        }

        [Theory]
        [InlineData("2023-02-01", "123 Maple Street")]
        [InlineData("2023-04-01", "456 Oak Avenue")]
        public void getting_an_address_in_a_specific_time(string date, string expectedAddress)
        {
            Clock.TravelThroughTime("2023-01-01");
            var customer = new Customer(1, "Olivia", "Johnson", "123 Maple Street", Clock);

            Clock.TravelThroughTime("2023-03-01");
            customer.ChangeAddress("456 Oak Avenue", Clock);

            var dateTime = DateTime.Parse(date);
            var actualAddress = customer.Address.EffectiveValueAt(dateTime);
            actualAddress.Should().Be(expectedAddress);
        }
    }
}