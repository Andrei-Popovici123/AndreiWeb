using AndreiWeb.DataAccess.Data;
using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;

namespace AndreiWeb.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext db): base(db)
    {
        _dbContext = db;
    }


    public void Update(Product product)
    {
        _dbContext.Update(product);
    }
}