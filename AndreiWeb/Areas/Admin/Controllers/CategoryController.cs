using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;
using AndreiWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AndreiWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var objCategoryList = _unitOfWork.Category.GetAll();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly Match the Name");
        }

        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index", "Category");
        }

        return View();
    }

    public IActionResult Edit(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }

        Category? categoryToEdit = _unitOfWork.Category.Get(u => u.Id == Id);
        if (categoryToEdit == null)
        {
            return NotFound();
        }

        return View(categoryToEdit);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction("Index", "Category");
        }

        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? categoryToEdit = _unitOfWork.Category.Get(category => category.Id == id);
        if (categoryToEdit == null)
        {
            return NotFound();
        }

        return View(categoryToEdit);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? obj = _unitOfWork.Category.Get(category => category.Id == id);
        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index", "Category");
    }
}