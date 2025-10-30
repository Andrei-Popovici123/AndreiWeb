using AndreiWeb.DataAccess.Data;
using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;

namespace AndreiWeb.DataAccess.Repository;

public class ApplicationUserRepository:  Repository<ApplicationUser>, IApplicationUserRepository
{
    private ApplicationDbContext _db;

    public ApplicationUserRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    } 
}