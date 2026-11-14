using System.Data;
using System.Linq;
using System.Web.Mvc;
using Warehouse_25DH110348.Models;

namespace Warehouse_25DH110348.Controllers
{
    public class TransfersController : Controller
    {
        private readonly WarehouseEntities db = new WarehouseEntities();

        public ActionResult Index()
        {
            var model = new TransferViewModel();
            PopulateLists(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TransferViewModel model)
        {
            if (!model.Quantity.HasValue || model.Quantity.Value <= 0)
                ModelState.AddModelError("Quantity", "Số lượng điều chuyển phải là số nguyên dương.");
            if (ModelState.IsValid && model.SourceProductID == model.DestinationProductID)
                ModelState.AddModelError("DestinationProductID", "Sản phẩm nguồn và đích phải khác nhau.");
            if (!ModelState.IsValid) { PopulateLists(model); return View(model); }

            using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    var source = db.Products.Find(model.SourceProductID.Value);
                    var destination = db.Products.Find(model.DestinationProductID.Value);
                    if (source == null || destination == null)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError("", "Không tìm thấy sản phẩm nguồn hoặc sản phẩm đích.");
                    }
                    else if (source.Category != destination.Category)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError("", "Chỉ điều chuyển giữa hai sản phẩm cùng danh mục.");
                    }
                    else if (source.Quantity < model.Quantity.Value)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError("", "Sản phẩm nguồn không đủ hàng; số lượng của cả hai sản phẩm được giữ nguyên.");
                    }
                    else
                    {
                        source.Quantity -= model.Quantity.Value;
                        checked { destination.Quantity += model.Quantity.Value; }
                        db.SaveChanges();
                        transaction.Commit();
                        TempData["Message"] = "Điều chuyển thành công. Nguồn còn " + source.Quantity + ", đích có " + destination.Quantity + ".";
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            PopulateLists(model);
            return View(model);
        }

        private System.Collections.Generic.IList<SelectListItem> ProductOptions()
        {
            return db.Products.OrderBy(p => p.ProductName).ToList()
                .Select(p => new SelectListItem { Value = p.ProductID.ToString(), Text = p.ProductName })
                .ToList();
        }

        private void PopulateLists(TransferViewModel model)
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
