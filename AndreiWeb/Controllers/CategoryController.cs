using AndreiWeb.Data;
using AndreiWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace AndreiWeb.Controllers;

public class CategoryController : Controller
{
    // GET
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var objCategoryList = _db.Categories.ToList();
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
            _db.Categories.Add(obj);
            _db.SaveChanges();
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

        Category? categoryToEdit = _db.Categories.FirstOrDefault(category => category.Id == Id);
        if (categoryToEdit == null)
        {
            return NotFound();
        }

        return View(categoryToEdit);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly Match the Name");
        }

        if (ModelState.IsValid)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        return View();
    }
}