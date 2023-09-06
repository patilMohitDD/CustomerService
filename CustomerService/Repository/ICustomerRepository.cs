using  CustomerService.Model;


namespace CustomerService.Repository{
    public interface ICustomerRepository{
        IEnumerable<Customer> GetCustomers();
        void AddCustomers(IEnumerable<Customer> newCustomers);
    }
}