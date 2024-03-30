using AGV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AGV.Data.Map
{
    public class RequestMap : IEntityTypeConfiguration<RequestModel>
    {
        public void Configure(EntityTypeBuilder<RequestModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.GoodsId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(255);
            builder.Property(x => x.Status).IsRequired();

            builder.HasOne(x => x.Goods);
        }
    }
}
