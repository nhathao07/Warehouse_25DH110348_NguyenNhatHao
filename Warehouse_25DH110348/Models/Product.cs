using System;

namespace Warehouse_25DH110348.Models
{
    // POCO generated from the EF Database First model (WarehouseModel.edmx).
    public partial class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
