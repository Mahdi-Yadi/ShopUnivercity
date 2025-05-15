using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopUnivercity.Web.Data;
using ShopUnivercity.Web.Tools;
namespace ShopUnivercity.Web.Areas.AdminPanel.Controllers;
[Authorize]
[Area("AdminPanel")]
[Route("Admin")]
public class OrdersController : Controller
{

    private readonly SiteDBContext _db;

    public OrdersController(SiteDBContext site)
    {
        _db = site;
    }

    [HttpGet("Orders")]
    public IActionResult Orders()
    {
        var orders = _db.Orders
          .Include(a => a.OrderDetails)
          .ThenInclude(a => a.Product)
                  .ToList();

        return View(orders);
    }

    [HttpGet("Order/{id}")]
    public IActionResult Order(int id)
    {
        var order = _db.Orders
           .Include(a => a.OrderDetails)
           .ThenInclude(a => a.Product)
           .Include(a => a.User)
           .FirstOrDefault(a => a.Id == id);

        if (order == null)
            return BadRequest();

        return View(order);
    }


    [HttpGet("CompleteOrder")]
    public IActionResult CompleteOrder(int id)
    {
        var order = _db.Orders
         .FirstOrDefault(a => a.Id == id);

        if (order == null)
            return BadRequest();

        order.IsCompleted = true;

        _db.Orders.Update(order);
        _db.SaveChanges();

        return RedirectToAction(nameof(order));
    }

}