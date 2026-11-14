using System.Linq;
using System.Web.Mvc;
using Warehouse_25DH110348.Models;

namespace Warehouse_25DH110348.Controllers
{
    public class InventoryController : Controller
    {
        private readonly WarehouseEntities db = new WarehouseEntities();

        public ActionResult Index()
        {
            var model = new StockAdjustmentViewModel();
            PopulateLists(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Restock(StockAdjustmentViewModel model)
        {
            model.Operation = "restock";
            if (!model.Amount.HasValue || model.Amount.Value <= 0)
                ModelState.AddModelError("Amount", "Số lượng nhập phải là số nguyên dương.");
            if (!ModelState.IsValid) { PopulateLists(model); return View("Index", model); }

            var product = db.Products.Find(model.ProductID.Value);
            if (product == null) return HttpNotFound("Không tìm thấy sản phẩm.");
            checked { product.Quantity += model.Amount.Value; }
            db.SaveChanges();
            TempData["Message"] = "Đã nhập kho. Số lượng mới: " + product.Quantity + ".";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Damaged(StockAdjustmentViewModel model)
        {
            model.Operation = "damaged";
            if (!model.Amount.HasValue || model.Amount.Value <= 0)
                ModelState.AddModelError("Amount", "Số lượng hỏng phải là số nguyên dương.");
            if (!ModelState.IsValid) { PopulateLists(model); return View("Index", model); }

            var product = db.Products.Find(model.ProductID.Value);
            if (product == null) return HttpNotFound("Không tìm thấy sản phẩm.");
            if (model.Amount.Value > product.Quantity)
            {
                ModelState.AddModelError("Amount", "Số lượng hỏng lớn hơn lượng hàng đang có; dữ liệu không thay đổi.");
                PopulateLists(model);
                return View("Index", model);
            }
            product.Quantity -= model.Amount.Value;
            db.SaveChanges();
            TempData["Message"] = "Đã ghi nhận hàng hỏng. Số lượng mới: " + product.Quantity + ".";
            return RedirectToAction("Index");
        }

        private System.Collections.Generic.IList<SelectListItem> ProductOptions()
        {
            return db.Products.OrderBy(p => p.ProductName).ToList()
                .Select(p => new SelectListItem { Value = p.ProductID.ToString(), Text = p.ProductName })
                .ToList();
        }

        private void PopulateLists(StockAdjustmentViewModel model)
        {
            model.Products = ProductOptions();
            model.CurrentStock = db.Products.OrderBy(p => p.ProductID).ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
