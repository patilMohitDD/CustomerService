using Microsoft.AspNetCore.Mvc;
using CustomerService.Model;
using CustomerService.Repository;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CustomerController : ControllerBase{ 
    private readonly ICustomerRepository customerRepository;

    public CustomerController(ICustomerRepository customerRepository){
        this.customerRepository = customerRepository;
    }

    [HttpGet]
    public IEnumerable<Customer> GetCustomers()
    {
        return customerRepository.GetCustomers();
        
    }

    [HttpPost()]
    public ActionResult<Customer> CreateCustomer([FromBody] IEnumerable<Customer> newCustomers)
   {
        try{
            customerRepository.AddCustomers(newCustomers);
            return Ok("Customers added successfully.");
        }
        catch (InvalidOperationException ex){
            return BadRequest($"Invalid Input provided: {ex.Message}");
        }
   }
}
}