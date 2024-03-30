using AGV.Data.Map;
using AGV.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AGV.Data
{
    public class AGVDBContext : DbContext
    {
        public AGVDBContext(DbContextOptions<AGVDBContext> options)
            : base(options)
        {

        }

        public DbSet<GoodsModel> Goods { get; set; }
        public DbSet<RequestModel> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GoodsMap());
            modelBuilder.ApplyConfiguration(new RequestMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
