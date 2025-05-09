using Microsoft.AspNetCore.Mvc;
using ShopUnivercity.Web.Data;

namespace ShopUnivercity.Web.Controllers
{
    public class ProductsController : Controller
    {


        private readonly SiteDBContext _db;

        public ProductsController(SiteDBContext db)
        {
            _db = db;
        }

        public IActionResult Products()
        {
            var products = _db.Product.OrderByDescending(a => a.CreateDate)
                .Take(10).ToList();


            return View(products);
        }

        public IActionResult Product(int id)
        {
            var p = _db.Product.FirstOrDefault(a => a.Id == id);

            if (p == null)
            {
                return Redirect("/");
            }

            return View(p);
        }

    }
}
