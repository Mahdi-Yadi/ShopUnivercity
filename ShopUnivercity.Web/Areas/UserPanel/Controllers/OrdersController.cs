using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ShopUnivercity.Web.Areas.UserPanel.Controllers;
[Authorize]
[Area("UserPanel")]
[Route("Panel")]
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


    [HttpGet("DeleteOrderDetail")]
    public IActionResult DeleteOrderDetail()
    {
        return Redirect("/");
    }

}