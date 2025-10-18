using Microsoft.AspNetCore.Mvc;

namespace AndreiWeb.Controllers;

public class CategoryController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}