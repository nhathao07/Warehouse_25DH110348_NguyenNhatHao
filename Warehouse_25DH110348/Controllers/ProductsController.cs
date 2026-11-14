using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Warehouse_25DH110348.Models;

namespace Warehouse_25DH110348.Controllers
{
    public class ProductsController : Controller
    {
        private readonly WarehouseEntities db = new WarehouseEntities();

        public ActionResult Index(string category, int? fromQuantity, int? toQuantity, string sortOrder)
        {
            var model = new ProductListViewModel
            {
                Category = category,
                FromQuantity = fromQuantity,
                ToQuantity = toQuantity,
                SortOrder = sortOrder,
                Categories = db.Products.Select(p => p.Category).Distinct().OrderBy(c => c)
                    .ToList().Select(c => new SelectListItem { Text = c, Value = c }).ToList()
            };

            if ((fromQuantity.HasValue && fromQuantity.Value < 0) ||
                (toQuantity.HasValue && toQuantity.Value < 0) ||
                (fromQuantity.HasValue && toQuantity.HasValue && fromQuantity.Value > toQuantity.Value))
            {
                ModelState.AddModelError("", "Khoảng số lượng không hợp lệ. Giá trị phải không âm và Từ phải nhỏ hơn hoặc bằng Đến.");
                model.Products = new Product[0];
                return View(model);
            }

            IQueryable<Product> query = db.Products;
            if (!String.IsNullOrWhiteSpace(category)) query = query.Where(p => p.Category == category);
            if (fromQuantity.HasValue) query = query.Where(p => p.Quantity >= fromQuantity.Value);
            if (toQuantity.HasValue) query = query.Where(p => p.Quantity <= toQuantity.Value);
            query = sortOrder == "quantity_desc"
                ? query.OrderByDescending(p => p.Quantity).ThenBy(p => p.ProductID)
                : query.OrderBy(p => p.Quantity).ThenBy(p => p.ProductID);
            model.Products = query.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new ProductCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateViewModel model)
        {
            if (model.ProductName != null) model.ProductName = model.ProductName.Trim();
            if (model.Category != null) model.Category = model.Category.Trim();
            if (String.IsNullOrWhiteSpace(model.ProductName)) ModelState.AddModelError("ProductName", "Tên sản phẩm không được để trống.");
            if (String.IsNullOrWhiteSpace(model.Category)) ModelState.AddModelError("Category", "Danh mục không được để trống.");
            if (!ModelState.IsValid) return View(model);

            db.Products.Add(new Product
            {
                ProductName = model.ProductName,
                Category = model.Category,
                Price = model.Price,
                Quantity = model.Quantity
            });
            db.SaveChanges();
            TempData["Message"] = "Đã thêm sản phẩm mới.";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var product = db.Products.Find(id.Value);
            if (product == null) return HttpNotFound("Không tìm thấy sản phẩm cần xóa.");
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, bool confirmed)
        {
            var product = db.Products.Find(id);
            if (product == null) return HttpNotFound("Sản phẩm không tồn tại hoặc đã được xóa.");
            if (!confirmed)
            {
                TempData["Error"] = "Bạn chưa xác nhận thao tác xóa.";
                return RedirectToAction("Index");
            }
            if (product.Quantity != 0)
            {
                TempData["Error"] = "Không thể xóa sản phẩm còn hàng. Chỉ sản phẩm có Quantity = 0 mới được xóa.";
                return RedirectToAction("Index");
            }
            db.Products.Remove(product);
            db.SaveChanges();
            TempData["Message"] = "Đã xóa sản phẩm hết hàng.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
