using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShopUnivercity.Web.Data;
using ShopUnivercity.Web.Models;

namespace ShopUnivercity.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiteDBContext _db;

        public HomeController(SiteDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.lastProducts = _db.Product.OrderByDescending(a => a.CreateDate)
                .Take(6).ToList();
            return View();
        }

       
    }
}
