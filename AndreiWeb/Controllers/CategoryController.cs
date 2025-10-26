using AndreiWeb.DataAccess.Data;
using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace AndreiWeb.Controllers;

public class CategoryController : Controller
{
    // GET
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index()
    {
        var objCategoryList = _categoryRepository.GetAll();
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
            _categoryRepository.Add(obj);
            _categoryRepository.Save();
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

        Category? categoryToEdit = _categoryRepository.Get(u => u.Id == Id);
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
            _categoryRepository.Update(obj);
            _categoryRepository.Save();
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

        Category? categoryToEdit = _categoryRepository.Get(category => category.Id == id);
        if (categoryToEdit == null)
        {
            return NotFound();
        }

        return View(categoryToEdit);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? obj = _categoryRepository.Get(category => category.Id == id);
        if (obj == null)
        {
            return NotFound();
        }

        _categoryRepository.Remove(obj);
        _categoryRepository.Save();
            TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index", "Category");
    }
}