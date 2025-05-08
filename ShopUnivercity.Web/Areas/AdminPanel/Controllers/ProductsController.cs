using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopUnivercity.Web.Data;
using ShopUnivercity.Web.Models.Entities.Products;

namespace ShopUnivercity.Web.Areas.AdminPanel.Controllers;
[Authorize]
[Area("AdminPanel")]
[Route("Admin")]
public class ProductsController : Controller
{
    private SiteDBContext _db;

    public ProductsController(SiteDBContext db)
    {
        _db = db;
    }

    [HttpGet("Products")]
    public IActionResult Products()
    {
        var p = _db.Product.ToList();

        return View(p);
    }


    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost("Create")]
    public IActionResult Create(Product p, IFormFile imageFile)
    {
        var oldP = _db.Product.FirstOrDefault(a => a.Title == p.Title);

        if (oldP != null)
        {
            ViewBag.message = "محصول از قبل ثبت شده است";
            return View(oldP);
        }

        Product newP = new Product();

        newP = p;

        if (imageFile != null)
        {
            try
            {
                if (imageFile.Length == 1000000)
                {
                    ViewBag.message = "حجم تصویر بالاست";
                    return View(oldP);
                }

                var imageName = p.Title.Replace(" ", "-") + Path.GetExtension(imageFile.FileName);

                string path = Path.Combine(Directory.GetCurrentDirectory()) + "wwwroot/Images/" + imageName;
                using (var stram = new FileStream(path, FileMode.Create))
                {
                    imageFile.CopyTo(stram);
                }

                newP.ImageName = imageName;

            }
            catch (Exception)
            {
                throw;
            }
        }

        _db.Product.Add(newP);
        _db.SaveChanges();

        ViewBag.message = "محصول با موفقیت ایجاد شد";

        return View();
    }

    [HttpGet("Edit/{id}")]
    public IActionResult Edit(int id)
    {
        var oldP = _db.Product.FirstOrDefault(a =>
                                                   a.Id == id);

        if (oldP == null)
        {
            return RedirectToAction(nameof(Products));
        }

        return View(oldP);
    }

    [HttpPost("Edit/{id}")]
    public IActionResult Edit(int id, Product p, IFormFile imageFile)
    {
        var oldP = _db.Product.FirstOrDefault(a => a.Title == p.Title && 
                                                   a.Id != p.Id);

        if (oldP != null)
        {
            ViewBag.message = "محصول از قبل ثبت شده است";
            return View(oldP);
        }

        var product = _db.Product.FirstOrDefault(a => a.Id == p.Id);

        product = p;

        if (imageFile != null)
        {
            try
            {
                if (imageFile.Length == 1000000)
                {
                    ViewBag.message = "حجم تصویر بالاست";
                    return View(oldP);
                }


                var imageName = p.Title.Replace(" ", "-") + Path.GetExtension(imageFile.FileName);

                string path = Path.Combine(Directory.GetCurrentDirectory()) + "wwwroot/Images/" + imageName;

                if (product.ImageName != null)
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory()) + "wwwroot/Images/" + product.ImageName);

                using (var stram = new FileStream(path, FileMode.Create))
                {
                    imageFile.CopyTo(stram);
                }

                product.ImageName = imageName;

            }
            catch (Exception)
            {
                throw;
            }
        }

        _db.Product.Update(product);
        _db.SaveChanges();

        ViewBag.message = "محصول با موفقیت بروزرسانی شد";

        return View();
    }


    [HttpGet("DeleteProduct/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var oldP = _db.Product.FirstOrDefault(a =>
            a.Id != id);

        if (oldP == null)
        {
            return RedirectToAction(nameof(Products));
        }

        return View(oldP);
    }

    [HttpPost("DeleteProduct/{id}")]
    public IActionResult DeleteProduct(int id,string title)
    {
        var product = _db.Product.FirstOrDefault(a => 
                                                   a.Id == id);

        product.IsDeleted = true;

        _db.Product.Update(product);
        _db.SaveChanges();

        ViewBag.message = "محصول با موفقیت حذف شد";

        return View();
    }

}