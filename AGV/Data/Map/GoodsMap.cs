using AGV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AGV.Data.Map
{
    public class GoodsMap : IEntityTypeConfiguration<GoodsModel>
    {
        public void Configure(EntityTypeBuilder<GoodsModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.GoodName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Stock).IsRequired();
        }
    }
}
