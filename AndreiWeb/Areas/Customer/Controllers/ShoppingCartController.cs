using System.Security.Claims;
using AndreiWeb.DataAccess.Repository.IRepository;
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
        
        return View(ShoppingCartViewModel);
    }
}