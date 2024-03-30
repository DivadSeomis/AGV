using AGV.Models;

namespace AGV.Repositories.Interfaces
{
    public interface IGoodsRepository
    {
        Task<List<GoodsModel>> GetAllGoods();

        Task<GoodsModel> GetGoodForId(int id);

        Task<GoodsModel> Add(GoodsModel good);

        Task<GoodsModel> Update(GoodsModel good, int id);

        Task<bool> Delete(int id);
    }
}
