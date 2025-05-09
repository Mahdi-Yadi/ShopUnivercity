using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopUnivercity.Web.Data;
using ShopUnivercity.Web.Tools;

namespace ShopUnivercity.Web.Areas.UserPanel.Controllers;
[Authorize]
[Area("UserPanel")]
[Route("Panel")]
public class HomeController : Controller
{
    private SiteDBContext _db;

    public HomeController(SiteDBContext db)
    {
        _db = db;
    }


    [HttpGet("Dashboard")]
    public IActionResult Index()
    {
        int userId = User.GetUserId();

        var user = _db.Users.FirstOrDefault(a => a.Id == userId);

        if (user.IsAdmin)
        {
            ViewBag.isAdmin = true;
        }

        return View();
    }
}