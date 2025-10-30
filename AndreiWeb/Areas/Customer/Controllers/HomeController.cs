using System.Diagnostics;
using System.Security.Claims;
using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AndreiWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
        return View(productList);
    }

    public IActionResult Details(int id)
    {
        ShoppingCart shoppingCart = new()
        {
            Product = _unitOfWork.Product.Get(product => product.Id == id, includeProperties: "Category"),
            Count = 1,
            ProductId = id
        };
        return View(shoppingCart);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        shoppingCart.ApplicationUserId = userId;

        ShoppingCart cartFormDB = _unitOfWork.ShoppingCart
            .Get(u => u.ApplicationUserId == userId && u.ProductId == shoppingCart.ProductId);

        if (cartFormDB != null)
        {
            //update shopping cart
            cartFormDB.Count += shoppingCart.Count;
            _unitOfWork.ShoppingCart.Update(cartFormDB);
        }
        else
        {
            // create shopping cart
            shoppingCart.Id = 0;
            _unitOfWork.ShoppingCart.Add(shoppingCart);
        }

        _unitOfWork.Save();

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}