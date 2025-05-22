using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parbad;
using Parbad.AspNetCore;
using Parbad.Gateway.ZarinPal;
using ShopUnivercity.Web.Data;
using ShopUnivercity.Web.Tools;

namespace ShopUnivercity.Web.Areas.UserPanel.Controllers;
[Authorize]
[Area("UserPanel")]
[Route("Panel")]
public class PaymentsController : Controller
{

    private readonly SiteDBContext _db;
    private readonly IOnlinePayment _onlinePayment;

    public PaymentsController(SiteDBContext db, IOnlinePayment onlinePayment)
    {
        _db = db;
        _onlinePayment = onlinePayment;
    }

    [HttpGet("Pay")]
    public async Task<IActionResult> Pay()
    {
        var order = _db.Orders
            .Include(a => a.OrderDetails)
            .ThenInclude(a => a.Product)
            .FirstOrDefault(a => !a.IsPay &&
        a.UserId == User.GetUserId());

        if (order == null)
            return BadRequest();

        float totalPrice = 0;

        if (order.OrderDetails.Count > 0)
        {
            foreach (var item in order.OrderDetails)
            {
                float price = (float)(item.Product.Price * item.Count);

                totalPrice += price;
            }
        }

        try
        {
            string trakingCode = $"{DateTime.Now:yyyyMMddHHmmmmssffff}";

            string callBack = "https://localhost:44357/Panel/VerifyPay";

            // تبدیل ریال به تومان برای درگاه پرداخت
            totalPrice *= 10;

            var resPayment = await _onlinePayment.RequestAsync(invoice =>
            {
                invoice.SetZarinPalData("خرید از سایت ما")
                .SetTrackingNumber(Convert.ToInt64(trakingCode))
                .SetAmount((Money)totalPrice)
                .SetCallbackUrl(callBack)
                .SetGateway("ZarinPal");
            });

            var resOrder = false;

            order.TrakingCode = trakingCode;

            _db.Orders.Update(order);
            _db.SaveChanges();

            resOrder = true;

            if (resOrder)
            {
                if(resPayment.IsSucceed)
                    return resPayment.GatewayTransporter.TransportToGateway();
            }

            return BadRequest();
        }
        catch (Exception)
        {
            return BadRequest();
            throw;
        }

    }

    [HttpPost("VerifyPay")]
    [HttpGet("VerifyPay")]
    public async Task<IActionResult> VerifyPay()
    {
        var invoice = await _onlinePayment.FetchAsync();

        ViewBag.Message = "";

        if (invoice == null)
        {
            ViewBag.Message = "خطایی از سمت درگاه پرداخت رخ داده!";
            return View();
        }


        if (invoice.Status != PaymentFetchResultStatus.ReadyForVerifying)
        {
            ViewBag.Message = "پرداخت صورت نگرفت!";
            return View();
        }

        var verifyResult = await _onlinePayment.VerifyAsync(invoice);

        if(verifyResult.Status == PaymentVerifyResultStatus.Succeed)
        {
            var order = _db.Orders
                .Include(a => a.OrderDetails)
                .ThenInclude(a => a.Product)
                .FirstOrDefault(a => a.TrakingCode == verifyResult.TrackingNumber.ToString());

            if(order == null)
            {
                ViewBag.Message = "فاکتوری یافت نشد!";
                return View();
            }

            order.PaymentCode = verifyResult.TransactionCode;
            order.PaymentDate = DateTime.Now;
            order.IsPay = true;

            decimal totalPrice = 0;

            foreach (var item in order.OrderDetails)
            {
                decimal price = item.Product.Price * item.Count;

                item.Price = item.Product.Price;

                _db.OrderDetails.Update(item);
                _db.SaveChanges();

                totalPrice += price;
            }

            order.SumPrice = totalPrice;

            _db.Orders.Update(order);
            _db.SaveChanges();


            ViewBag.Message = $"فاکتور با موفقیت پرداخت شد. {verifyResult.TransactionCode} کد پیگیری درگاه پرداخت.";

            return View();
        }
        else
        {
            return View();
        }   

    }

}