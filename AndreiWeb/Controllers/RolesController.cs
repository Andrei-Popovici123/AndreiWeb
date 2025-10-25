using AndreiWeb.DataAccess.Data;
using AndreiWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace AndreiWeb.Controllers;

public class RolesController : Controller
{
    // GET
    private readonly ApplicationDbContext _db;
    public RolesController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult ListRoles()
    {
        var objRolesListAllNotAdmin = _db.Roles.Where(role => role.Name != RoleName.Admin)
                                        .ToList();
        return View(objRolesListAllNotAdmin);
    }
}