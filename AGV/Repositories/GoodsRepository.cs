using AGV.Data;
using AGV.Models;
using AGV.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AGV.Repositories
{
    public class GoodsRepository : IGoodsRepository
    {
        private readonly AGVDBContext _dbContext;
        public GoodsRepository(AGVDBContext agvDBContext) {
            _dbContext = agvDBContext;
        }

        public async Task<List<GoodsModel>> GetAllGoods()
        {
            return await _dbContext.Goods.ToListAsync();
        }

        public async Task<GoodsModel> GetGoodForId(int id)
        {
            return await _dbContext.Goods.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GoodsModel> Add(GoodsModel good)
        {
            await _dbContext.Goods.AddAsync(good);
            await _dbContext.SaveChangesAsync();

            return good;
        }

        public async Task<GoodsModel> Update(GoodsModel good, int id)
        {
            GoodsModel goodForId = await GetGoodForId(id);

            if(goodForId == null)
            {
                throw new Exception($"Good with ID: {id} not found!");
            }

            goodForId.GoodName = good.GoodName;
            goodForId.Stock = good.Stock;

            _dbContext.Goods.Update(goodForId);
            await _dbContext.SaveChangesAsync();

            return good;
        }

        public async Task<bool> Delete(int id)
        {
            GoodsModel goodForId = await GetGoodForId(id);

            if (goodForId == null)
            {
                throw new Exception($"Good with ID: {id} not found!");
            }

            _dbContext.Goods.Remove(goodForId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        
    }
}
