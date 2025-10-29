using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;
using AndreiWeb.Models.ViewModels;
using AndreiWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AndreiWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]

public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View(_unitOfWork.Product.GetAll(includeProperties: "Category").ToList());
    }

    public IActionResult Upsert(int? id)
    {
        // Create op is being processed with View Model Consisting of Product and Category List
        ProductViewModel productViewModel = new()
        {
            CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            }),
            Product = new Product(),
        };
        if (id == null || id == 0)
        {
            return View(productViewModel);
        }
        else
        {
            productViewModel.Product = _unitOfWork.Product.Get(u => u.Id == id);
            return View(productViewModel);
        }
    }

    [HttpPost]
    public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");
                if (!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
                {
                    //delete the old image
                    var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productViewModel.Product.ImageUrl = @"\images\product\" + filename;
            }

            if (productViewModel.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productViewModel.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productViewModel.Product);
            }

            _unitOfWork.Save();
            TempData["success"] = "New Product Added Succesfully";
            return RedirectToAction("Index", "Product");
        }

        productViewModel.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString(),
        });

        return View(productViewModel);
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new { data = objProductList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
        if (productToBeDeleted == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }

        _unitOfWork.Product.Remove(productToBeDeleted);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });
    }

    #endregion
}