using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopUnivercity.Web.Data;
namespace ShopUnivercity.Web.Areas.AdminPanel.Controllers;
[Authorize]
[Area("AdminPanel")]
[Route("Admin")]
public class UsersController : Controller
{
    private readonly SiteDBContext _db;

    public UsersController(SiteDBContext db)
    {
        _db = db;
    }

    [HttpGet("UsersList")]
    public IActionResult UsersList()
    {
        var users = _db.Users.ToList();

        if (users.Count == 0)
            return Redirect("/");

        return View(users);
    }

    [HttpGet("UserInfo/{userId}")]
    public IActionResult UserInfo(int userId)
    {
        var user = _db.Users
            .Include(a => a.Orders)
            .ThenInclude(a => a.OrderDetails)
            .ThenInclude(a => a.Product)
            .FirstOrDefault(a => a.Id == userId);

        if (user == null)
            return Redirect("/");

        return View(user);
    }

}
