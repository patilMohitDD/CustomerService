using CustomerService.Model;
using Newtonsoft.Json;

namespace CustomerService.Repository{
    
    public class CustomerRepository : ICustomerRepository
    { 
        private readonly List<Customer> customers = new List<Customer>();
        private readonly string dataFilePath = "customerData.json";

        public CustomerRepository()
        {
            LoadData();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return customers;
        }

        
        public void AddCustomers(IEnumerable<Customer> newCustomers)
        {
            try{
                foreach (var customer in newCustomers)
                {
                    if (ValidateCustomer(customer))
                    {
                        InsertSorted(customer);
                    }
                    else{
                        throw new InvalidOperationException("Something wrong in Customer Data field");
                    }
                }

                SaveData();
            }
            catch (InvalidOperationException){ 
                throw;
            }
            
        }

        private bool ValidateCustomer(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.FirstName) ||
                string.IsNullOrEmpty(customer.LastName) ||
                customer.Age <= 18 || customer.Age > 90 ||
                customers.Any(c => c.Id == customer.Id))
            {
                return false;
            }
            return true;
        }

        private void InsertSorted(Customer newCustomer)
        {
            int index = 0;
            while (index < customers.Count && string.Compare(newCustomer.LastName, customers[index].LastName, StringComparison.Ordinal) > 0)
            {
                index++;
            }

            while (index < customers.Count && string.Compare(newCustomer.LastName, customers[index].LastName, StringComparison.Ordinal) == 0 && string.Compare(newCustomer.FirstName, customers[index].FirstName, StringComparison.Ordinal) > 0)
            {
                index++;
            }

            customers.Insert(index, newCustomer);
        }

        private void LoadData()
        {
            if (File.Exists(dataFilePath))
            {
                var data = File.ReadAllText(dataFilePath);
                customers.AddRange(JsonConvert.DeserializeObject<List<Customer>>(data));
            }
        }

        private void SaveData()
        {
            var data = JsonConvert.SerializeObject(customers);
            File.WriteAllText(dataFilePath, data);
        }
    }
}