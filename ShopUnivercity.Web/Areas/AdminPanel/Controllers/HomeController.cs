using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ShopUnivercity.Web.Areas.AdminPanel.Controllers;
[Authorize]
[Area("AdminPanel")]
[Route("Admin")]
public class HomeController : Controller
{

    [HttpGet("index")]
    public IActionResult Index()
    {
        return View();
    }
}