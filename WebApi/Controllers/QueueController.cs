using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<QueueModel>> GetAll()
        {
            IEnumerable<QueueModel> queueModels = _queueService.GetAll();
            return Ok(queueModels);
        }

        [HttpGet("{id}")]
        public ActionResult<QueueModel> GetGoodById(int id)
        {
            QueueModel queueModel = _queueService.Get(id);

            return Ok(queueModel);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            _queueService.Delete(id);
            return NoContent();
        }
    }
}
