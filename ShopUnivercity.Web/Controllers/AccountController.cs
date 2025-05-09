using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ShopUnivercity.Web.Data;
using ShopUnivercity.Web.Tools;
using System.Security.Claims;

namespace ShopUnivercity.Web.Controllers;
public class AccountController : Controller
{
    private SiteDBContext _db;

    public AccountController(SiteDBContext db)
    {
        _db = db;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string phone, string password)
    {
        if (phone == null || password == null)
        {
            ViewBag.message = "شماره تلفن یا کلمه عبور را وارد نمایید";
            return View();
        }

        var user = _db.Users.FirstOrDefault(a => a.PhoneNumber == phone);

        if (user != null)
        {
            ViewBag.message = "شماره تلفن وارد شده تکراری است";
            return View();
        }

        Models.Entities.Account.User us = new Models.Entities.Account.User();

        us.PhoneNumber = phone;
        us.FullName = phone;
        us.Password = Hashing.HashPassword(password);
        us.Address = "ندارد";
        us.Email = "ندارد";
        us.CodePost = "ندارد";
        us.CreateDate = DateTime.Now;

        _db.Users.Add(us);
        _db.SaveChanges();

        ViewBag.message = "کاربر مورد نظر با موفقیت ثبت نام شد";

        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(string phone, string password,bool remember)
    {
        if (phone == null || password == null)
        {
            ViewBag.message = "شماره تلفن یا کلمه عبور را وارد نمایید";
            return View();
        }

        var user = _db.Users.FirstOrDefault(a => a.PhoneNumber == phone && a.Password == Hashing.HashPassword(password));

        if (user == null)
        {
            ViewBag.message = "شماره تلفن وارد شده ثبت نشده است";
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.PhoneNumber),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var properties = new AuthenticationProperties
        {
            IsPersistent = remember
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            properties);

        ViewBag.message = "ورود با موفقیت انجام شد.";

        return Redirect("/");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }

    public IActionResult Forgot()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Forgot(string phone)
    {
        if (phone == null)
        {
            ViewBag.message = "شماره تلفن را وارد نمایید";
            return View();
        }

        var user = _db.Users.FirstOrDefault(a => a.PhoneNumber == phone);

        if (user == null)
        {
            ViewBag.message = "شماره تلفن وارد شده ثبت نشده است";
            return View();
        }

        // Send SMS
        Random random = new Random();

        user.ActiveCode = random.Next(9999,99999);

        _db.Users.Update(user);
        _db.SaveChanges();

        // service sms

        return RedirectToAction(nameof(ResetPassword));
    }

    public IActionResult ResetPassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ResetPassword(int activeCode,string newPassword,string phone)
    {
        if (phone == null)
        {
            ViewBag.message = "شماره تلفن را وارد نمایید";
            return View();
        }

        if (newPassword == null)
        {
            ViewBag.message = "کلمه عبور را وارد نمایید";
            return View();
        }

        if (activeCode == null)
        {
            ViewBag.message = "کد فعال سازی را وارد نمایید";
            return View();
        }

        var user = _db.Users.FirstOrDefault(a => a.PhoneNumber == phone &&
                                                 a.ActiveCode == activeCode);

        if (user == null)
        {
            ViewBag.message = "شماره تلفن وارد شده ثبت نشده است";
            return View();
        }

        user.Password = Hashing.HashPassword(newPassword);

        _db.Users.Update(user);
        _db.SaveChanges();

        ViewBag.message = "کلمه عبور شما با موفقیت بروز شد.";

        return View();
    }

}