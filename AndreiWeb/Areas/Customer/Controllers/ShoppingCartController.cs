using Microsoft.AspNetCore.Mvc;

namespace AndreiWeb.Areas.Customer.Controllers;
[Area("Customer")]
public class ShoppingCartController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}