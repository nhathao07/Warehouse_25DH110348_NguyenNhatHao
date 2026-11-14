using System.Data.Entity;

namespace Warehouse_25DH110348.Models
{
    // EF6 DbContext generated from WarehouseModel.edmx (Database First).
    public partial class WarehouseEntities : DbContext
    {
        public WarehouseEntities() : base("name=WarehouseEntities") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new System.Data.Entity.Infrastructure.UnintentionalCodeFirstException();
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
