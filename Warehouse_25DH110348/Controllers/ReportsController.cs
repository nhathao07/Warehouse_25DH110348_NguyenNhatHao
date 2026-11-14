using System.Linq;
using System.Web.Mvc;
using Warehouse_25DH110348.Models;

namespace Warehouse_25DH110348.Controllers
{
    public class ReportsController : Controller
    {
        private readonly WarehouseEntities db = new WarehouseEntities();

        public ActionResult Index()
        {
            var stats = new InventoryStatsViewModel
            {
                ProductCount = db.Products.Count(),
                TotalQuantity = db.Products.Select(p => p.Quantity).DefaultIfEmpty(0).Sum(),
                LowStockCount = db.Products.Count(p => p.Quantity >= 1 && p.Quantity <= 4)
            };
            return View(stats);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
