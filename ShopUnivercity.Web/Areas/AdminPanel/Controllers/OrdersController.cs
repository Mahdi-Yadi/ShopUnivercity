using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ShopUnivercity.Web.Areas.AdminPanel.Controllers;
[Authorize]
[Area("AdminPanel")]
[Route("Admin")]
public class OrdersController : Controller
{

    [HttpGet("Orders")]
    public IActionResult Orders()
    {
        return View();
    }

    [HttpGet("Order")]
    public IActionResult Order()
    {
        return View();
    }

    [HttpGet("CompleteOrder")]
    public IActionResult CompleteOrder()
    {
        return View();
    }

}