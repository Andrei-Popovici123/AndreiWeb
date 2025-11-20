using System.Security.Claims;
using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;
using AndreiWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AndreiWeb.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class ShoppingCartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ShoppingCartViewModel ShoppingCartViewModel { get; set; }

    public ShoppingCartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET
    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartViewModel = new()
        {
            ShoppingCartList =
                _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userID, includeProperties: "Product")
        };

        foreach (var cart in ShoppingCartViewModel.ShoppingCartList)
        {
            cart.Price = GetPriceBasedOnQuantity(cart);
            ShoppingCartViewModel.OrderTotal += (cart.Price * cart.Count);
        }

        return View(ShoppingCartViewModel);
    }

    public IActionResult Increase(int cartId)
    {
        var cartFormDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
        cartFormDb.Count += 1;
        _unitOfWork.ShoppingCart.Update(cartFormDb);
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Decrease(int cartId)
    {
        var cartFormDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);

        if (cartFormDb.Count <= 1)
            _unitOfWork.ShoppingCart.Remove(cartFormDb);
        else
        {
            cartFormDb.Count -= 1;


            _unitOfWork.ShoppingCart.Update(cartFormDb);
        }

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Remove(int cartId)
    {
        var cartFormDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);

        _unitOfWork.ShoppingCart.Remove(cartFormDb);

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Summary()
    {
        return View();
    }
    private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
    {
        if (shoppingCart.Count <= 50)
            return shoppingCart.Product.Price;

        if (shoppingCart.Count <= 100)
            return shoppingCart.Product.Price50;

        return shoppingCart.Product.Price100;
    }
}