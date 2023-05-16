using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfGoodController : ControllerBase
    {
        private ITypeOfGoodService _typeOfGoodService;

        public TypeOfGoodController(ITypeOfGoodService typeOfGoodService)
        {
            _typeOfGoodService = typeOfGoodService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TypeOfGoodModel>> GetAll()
        {
            IEnumerable<TypeOfGoodModel> typeOfGoodModels = _typeOfGoodService.GetAll();
            return Ok(typeOfGoodModels);
        }

        [HttpGet("{id}")]
        public ActionResult<TypeOfGoodModel> GetGoodById(int id)
        {
            TypeOfGoodModel typeOfGoodModel = _typeOfGoodService.Get(id);

            return Ok(typeOfGoodModel);
        }

        [HttpPost]
        public ActionResult CreateType(TypeOfGoodRequest typeOfGoodRequest)
        {
            _typeOfGoodService.Create(typeOfGoodRequest);

            return Ok();
        }   

        [HttpPut("{id}")]
        public ActionResult UpdateType(int id, [FromBody] TypeOfGoodRequest typeOfGoodRequest)
        {
            _typeOfGoodService.Update(typeOfGoodRequest, id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            _typeOfGoodService.Delete(id);
            return NoContent();
        }
    }
}
