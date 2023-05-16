using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerModel>> GetAll()
        {
            IEnumerable<CustomerModel> customerModels = _customerService.GetAll();
            return Ok(customerModels);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerModel> GetGoodById(int id)
        {
            CustomerModel customerModel = _customerService.Get(id);

            return Ok(customerModel);
        }

        [HttpPost]
        public ActionResult CreateCustomer(CustomerRequest customerRequest)
        {
            _customerService.Create(customerRequest);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(int id, [FromBody] CustomerRequest customerRequest)
        {
            _customerService.Update(customerRequest, id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            _customerService.Delete(id);
            return NoContent();
        }
    }
}
