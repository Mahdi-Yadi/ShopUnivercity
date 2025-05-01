using Microsoft.AspNetCore.Mvc;

namespace ShopUnivercity.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Products()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }

    }
}
