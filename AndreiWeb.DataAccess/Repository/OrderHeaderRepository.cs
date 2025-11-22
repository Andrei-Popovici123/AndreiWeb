using AndreiWeb.DataAccess.Data;
using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;

namespace AndreiWeb.DataAccess.Repository;

public class OrderHeaderRepository :Repository<OrderHeader>, IOrderHeaderRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public OrderHeaderRepository(ApplicationDbContext db) : base(db)
    {
        _dbContext = db;
    }

    public void Update(OrderHeader orderHeader)
    {
        _dbContext.Update(orderHeader);
    }
}