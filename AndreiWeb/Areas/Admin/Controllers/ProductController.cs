using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        return View(_unitOfWork.Product.GetAll());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (product.Key.Length < 10)
        {
            ModelState.AddModelError("key", "The Key Must Be More Than 10 Characters");
        }

        if (product.Price < 0)
        {
            ModelState.AddModelError("Price", "Can't have a negative Price");
        }

        if (product.Price50 < 0)
        {
            ModelState.AddModelError("Price50", "Can't have a negative Price50");
        }

        if (product.Price100 < 0)
        {
            ModelState.AddModelError("Price100", "Can't have a negative Price100");
        }

        if (product.ListPrice < 0)
        {
            ModelState.AddModelError("ListPrice", "Can't have a negative ListPrice");
        }

        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Add(product);
            _unitOfWork.Save();
            TempData["success"] = "New Product Added Succesfully";
            return RedirectToAction("Index", "Product");
        }

        return View();
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
        if (product==null)
        {
            return NotFound();
        }
        
        return View(product);
    }    
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Product? product = _unitOfWork.Product.Get(product => product.Id == id);
        if (product==null)
        {
            return NotFound();
        }
        _unitOfWork.Product.Remove(product);
        _unitOfWork.Save();
            TempData["success"] = "Product Deleted successfully";
        return RedirectToAction("Index","Product");
    }    
}   
 