using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopUnivercity.Web.Data;

namespace ShopUnivercity.Web.Areas.AdminPanel.Controllers;
[Authorize]
[Area("AdminPanel")]
[Route("Admin")]
public class HomeController : Controller
{
    private readonly SiteDBContext _db;

    public HomeController(SiteDBContext db)
    {
        _db = db;
    }

    [HttpGet("index")]
    public IActionResult Index()
    {
        ViewBag.orders = _db.Orders.Count();
        ViewBag.users = _db.Users.Count();
        ViewBag.products = _db.Product.Count();

        return View();
    }
}