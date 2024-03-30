using AGV.Models;
using AGV.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AGV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsRepository _goodsRepository;

        public GoodsController(IGoodsRepository goodsRepository)
        {
            _goodsRepository = goodsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<GoodsModel>>> GetAllGoods()
        {
            List<GoodsModel> goodsList = await _goodsRepository.GetAllGoods();
            return Ok(goodsList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GoodsModel>> GetGoodForId(int id)
        {
            GoodsModel good = await _goodsRepository.GetGoodForId(id);
            return Ok(good);
        }

        [HttpPost]
        public async Task<ActionResult<GoodsModel>> AddGood([FromBody] GoodsModel goodsModel)
        {
            GoodsModel good = await _goodsRepository.Add(goodsModel);
            return Ok(good);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GoodsModel>> UpdateGood([FromBody] GoodsModel goodsModel, int id)
        {
            goodsModel.Id = id;
            GoodsModel good = await _goodsRepository.Update(goodsModel, id);
            return Ok(good);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GoodsModel>> DeleteGood(int id)
        {
            bool delete = await _goodsRepository.Delete(id);
            return Ok(delete);
        } 
    }
}
