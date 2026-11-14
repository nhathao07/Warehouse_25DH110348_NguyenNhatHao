using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Warehouse_25DH110348.Models
{
    public class ProductListViewModel
    {
        public IList<Product> Products { get; set; }
        public IList<SelectListItem> Categories { get; set; }
        public string Category { get; set; }
        public int? FromQuantity { get; set; }
        public int? ToQuantity { get; set; }
        public string SortOrder { get; set; }
    }

    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm.")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm tối đa 100 ký tự.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập danh mục.")]
        [StringLength(100, ErrorMessage = "Danh mục tối đa 100 ký tự.")]
        public string Category { get; set; }

        [Range(typeof(decimal), "0.01", "9999999999999999.99", ErrorMessage = "Giá phải lớn hơn 0.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là số nguyên không âm.")]
        public int Quantity { get; set; }
    }

    public class StockAdjustmentViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn sản phẩm.")]
        public int? ProductID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng.")]
        public int? Amount { get; set; }

        public IList<SelectListItem> Products { get; set; }
        public IList<Product> CurrentStock { get; set; }
        public string Operation { get; set; }
    }

    public class TransferViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn sản phẩm nguồn.")]
        public int? SourceProductID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn sản phẩm đích.")]
        public int? DestinationProductID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng chuyển.")]
        public int? Quantity { get; set; }

        public IList<SelectListItem> Products { get; set; }
        public IList<Product> CurrentStock { get; set; }
    }

    public class InventoryStatsViewModel
    {
        public int ProductCount { get; set; }
        public int TotalQuantity { get; set; }
        public int LowStockCount { get; set; }
    }
}
