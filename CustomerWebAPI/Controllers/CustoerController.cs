using CustomerWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustoerController : ControllerBase
    {
        private readonly CustomerDbContext _customerDbContext;

        public CustoerController(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return _customerDbContext.Customers;
        }

        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetById(int customerId)
        {
            var customer = await _customerDbContext.Customers.FindAsync(customerId);

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            await _customerDbContext.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Customer customer)
        {
            _customerDbContext.Customers.Update(customer);
            await _customerDbContext.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpDelete("{customerId:int}")]
        public async Task<ActionResult> Delete(int customerId)
        {
            var customer = await _customerDbContext.Customers.FindAsync(customerId);
            _customerDbContext.Customers.Remove(customer);  
            await _customerDbContext.SaveChangesAsync();
            return Ok(customer);
        }
    }
}
