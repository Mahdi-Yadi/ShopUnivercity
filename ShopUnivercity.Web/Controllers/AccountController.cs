using Microsoft.AspNetCore.Mvc;

namespace ShopUnivercity.Web.Controllers;

    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult Forgot()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

}
