using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using BLL.Interfaces;
using BLL.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodController : ControllerBase
    {
        private IGoodService _goodService;

        public GoodController(IGoodService goodService)
        {
            _goodService = goodService;         
        }

        [HttpGet]
        public ActionResult<IEnumerable<GoodModel>> GetAll()
        {
            IEnumerable<GoodModel> goodModels =  _goodService.GetAll();
            return Ok(goodModels);
        }

        [HttpGet("{id}")]
        public ActionResult<GoodModel> GetGoodById(int id)
        {
            GoodModel goodModel = _goodService.Get(id);

            return Ok(goodModel);
        }

        [HttpPost]
        public ActionResult CreateGood(GoodsRequestToCreate goodsRequest)
        {
             _goodService.Create(goodsRequest);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateGood(int id, [FromBody] GoodsRequest goodsRequest)
        {
             _goodService.Update(goodsRequest, id);
             
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
             _goodService.Delete(id);
            return NoContent();
        }
    }
}
