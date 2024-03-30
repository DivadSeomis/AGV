using AGV.Enums;

namespace AGV.Models
{
    public class RequestModel
    {
        public int Id { get; set; }
        public int Quantity {  get; set; }
        public string? Description { get; set; }
        public StatusRequest Status { get; set; }
        public int GoodsId { get; set; }

        public virtual GoodsModel? Goods { get; set; }
    }
}
