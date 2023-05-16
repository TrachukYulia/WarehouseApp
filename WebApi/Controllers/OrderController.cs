using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderModel>> GetAll()
        {
            IEnumerable<OrderModel> orderModels = _orderService.GetAll();
            return Ok(orderModels);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderModel> GetGoodById(int id)
        {
            OrderModel orderModel = _orderService.Get(id);

            return Ok(orderModel);
        }

        [HttpPost]
        public ActionResult CreateOrder(OrderRequest orderModel)
        {
            _orderService.Create(orderModel);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, [FromBody] OrderGoodRequest orderModel)
        {
            _orderService.Update(orderModel, id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            _orderService.Delete(id);
            return NoContent();
        }
    }
}
