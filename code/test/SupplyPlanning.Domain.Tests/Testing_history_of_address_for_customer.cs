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
            var customer = new Customer(1, "Hadi", "Ahmadi", "Tehran, Iran");

            customer.Address.GetValue().Should().Be("Tehran, Iran");
        }

        [Fact]
        public void getting_the_current_address_after_changing_the_address()
        {
            Clock.TravelTo("2023-01-01");
            var customer = new Customer(1, "Hadi", "Ahmadi", "Tehran, Iran", Clock);

            Clock.TravelTo("2023-03-01");
            customer.ChangeAddress("Karaj, Iran", Clock);

            var actualAddress = customer.Address.GetValue();
            actualAddress.Should().Be("Karaj, Iran");
        }

        [Theory]
        [InlineData("2023-02-01", "Tehran, Iran")]
        [InlineData("2023-04-01", "Karaj, Iran")]
        public void getting_an_address_in_a_specific_time(string date, string expectedAddress)
        {
            Clock.TravelTo("2023-01-01");
            var customer = new Customer(1, "Hadi", "Ahmadi", "Tehran, Iran", Clock);

            Clock.TravelTo("2023-03-01");
            customer.ChangeAddress("Karaj, Iran", Clock);

            var dateTime = DateTime.Parse(date);
            var actualAddress = customer.Address.EffectiveValueAt(dateTime);
            actualAddress.Should().Be(expectedAddress);
        }
    }
}