using AndreiWeb.DataAccess.Data;
using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;

namespace AndreiWeb.DataAccess.Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>,IShoppingCartRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ShoppingCartRepository(ApplicationDbContext db): base(db)
    {
        _dbContext = db;
    }


    public void Update(ShoppingCart shoppingCart)
    {
        _dbContext.Update(shoppingCart);
    }
}