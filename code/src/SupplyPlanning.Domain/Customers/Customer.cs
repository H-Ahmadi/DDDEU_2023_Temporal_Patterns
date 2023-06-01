using SupplyPlanning.Domain.Core;
using DateTime = System.DateTime;

namespace SupplyPlanning.Domain.Customers
{
    public class Customer
    {
        private HistoricalData<string> _address = new();
        public long Id { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public IReadonlyHistoricalData<string> Address => _address;

        public Customer(long id, string firstname, string lastname, string address) 
            : this(id, firstname, lastname, address, SystemClock.Instance)
        {
        }

        public Customer(long id, string firstname, string lastname, string address, IClock clock)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            _address.Set(address, clock.Now());
        }
        public void ChangeAddress(string address, IClock clock)
        {
            _address.Set(address, clock.Now());
        }
    }
}