using SupplyPlanning.Domain.Core;
using DateTime = System.DateTime;

namespace SupplyPlanning.Domain.Customers
{
    public class Customer 
    {
        public long Id { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public HistoricalData<string> Address { get; private set; } = new();

        public Customer(long id, string firstname, string lastname, string address) 
            : this(id, firstname, lastname, address, SystemClock.Instance)
        {
        }

        public Customer(long id, string firstname, string lastname, string address, IClock clock)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            this.Address.Set(address, clock.Now());
        }
        public void ChangeAddress(string address, IClock clock)
        {
            this.Address.Set(address, clock.Now());
        }
    }
}