using AndreiWeb.DataAccess.Data;
using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;

namespace AndreiWeb.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext context): base(context)
    {
        _dbContext = context;
    }


    public void Update(Product product)
    {
        _dbContext.Update(product);
    }
}