using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopUnivercity.Web.Data;
using ShopUnivercity.Web.Models.Entities.Orders;
using ShopUnivercity.Web.Tools;
namespace ShopUnivercity.Web.Areas.UserPanel.Controllers;
[Authorize]
[Area("UserPanel")]
[Route("Panel")]
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
            .Where(a => a.UserId == User.GetUserId())
            .ToList();

        return View(orders);
    }

    [HttpGet("Order/{id}")]
    public IActionResult Order(int id)
    {
        var order = _db.Orders
           .Include(a => a.OrderDetails)
           .ThenInclude(a => a.Product)
           .FirstOrDefault(a => a.Id == id);

        if (order == null)
            return BadRequest();

        return View(order);
    }

    [HttpGet("DeleteOrderDetail/{id}")]
    public IActionResult DeleteOrderDetail(int id)
    {
        var orderDetail = _db.OrderDetails
         .FirstOrDefault(a => a.Id == id);

        if (orderDetail == null)
            return BadRequest();

        _db.OrderDetails.Remove(orderDetail);
        _db.SaveChanges();

        return RedirectToAction(nameof(Orders));
    }


    [HttpPost]
    public IActionResult AddOrder(long id,int count)
    {
        var product = _db.Product.FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            return BadRequest();
        }

        int userId = User.GetUserId();

        var order = _db.Orders.FirstOrDefault(x => !x.IsPay &&
        x.UserId == userId);
        string mes = "";

        if (order == null)
        {
            Order o = new Order();

            o.SumPrice = 0;
            o.IsPay = false;
            o.UserId = userId;
            o.CreateDate = DateTime.Now;

            mes = "محصول به سبد خرید اضافه نشد.";

            _db.Orders.Add(o);
            _db.SaveChanges();

            OrderDetail detail = new OrderDetail();

            detail.ProductId = product.Id;
            detail.Count = count;
            detail.OrderId = o.Id;

            _db.OrderDetails.Add(detail);
            _db.SaveChanges();

            mes = "محصول به سبد خرید اضافه شد.";

            return Redirect($"/Products/Product/{id}");
        }
        else
        {
            var od = _db.OrderDetails.FirstOrDefault(a => a.OrderId == order.Id
            && a.ProductId == product.Id);

            if(od != null)
            {
                od.Count += count;
               
                _db.OrderDetails.Update(od);
                _db.SaveChanges();
            }
            else
            {
                OrderDetail detail = new OrderDetail();

                detail.ProductId = product.Id;
                detail.Count = count;
                detail.OrderId = order.Id;

                _db.OrderDetails.Add(detail);
                _db.SaveChanges();
            }
            return Redirect($"/Products/Product/{id}");
        }

    }

}