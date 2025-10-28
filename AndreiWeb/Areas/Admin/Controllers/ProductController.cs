using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;
using AndreiWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AndreiWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View(_unitOfWork.Product.GetAll().ToList());
    }

    public IActionResult Create()
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
        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Create(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Add(productViewModel.Product);
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


    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product? product = _unitOfWork.Product.Get(product => product.Id == id);
        if (product == null) return NotFound();

        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
            TempData["success"] = "Product Updated successfully";
            return RedirectToAction("Index", "Product");
        }

        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product? product = _unitOfWork.Product.Get(product => product.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Product? product = _unitOfWork.Product.Get(product => product.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        _unitOfWork.Product.Remove(product);
        _unitOfWork.Save();
        TempData["success"] = "Product Deleted successfully";
        return RedirectToAction("Index", "Product");
    }
}