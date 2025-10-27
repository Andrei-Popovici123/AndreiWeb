using AndreiWeb.Models;

namespace AndreiWeb.DataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product product);
}