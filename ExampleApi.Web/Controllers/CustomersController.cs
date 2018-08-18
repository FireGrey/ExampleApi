using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleApi.Domain.Model;
using ExampleApi.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<CustomerDto> _repository;

        public CustomersController(IRepository<CustomerDto> repository)
        {
            _repository = repository;
        }

        // GET api/customers
        [HttpGet(Name = "GetCustomers")]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
        {
            var customers = _repository.Query();

            return Ok(customers.ToList());
        }

        // GET api/customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public ActionResult<CustomerDto> GetCustomer(long id)
        {
            var result = _repository.Get(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST api/customers
        [HttpPost(Name = "AddCustomer")]
        public IActionResult AddCustomer([FromBody] CustomerDto value)
        {
            var result = _repository.Add(value);

            return CreatedAtRoute("GetCustomer", new { id = result.Id }, result);
        }

        // PUT api/customers/5
        [HttpPut("{id}", Name = "ReplaceCustomer")]
        public ActionResult<CustomerDto> ReplaceCustomer(int id, [FromBody] CustomerDto value)
        {
            var result = _repository.Replace(id, value);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/customers/5
        [HttpDelete("{id}", Name = "DeleteCustomer")]
        public ActionResult DeleteCustomer(long id)
        {
            var success = _repository.Delete(id);

            if(!success)
                return NotFound();

            return NoContent();
        }
    }
}
