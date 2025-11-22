using AndreiWeb.DataAccess.Data;
using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;

namespace AndreiWeb.DataAccess.Repository;

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    private readonly ApplicationDbContext _dbContext;
    public OrderDetailRepository(ApplicationDbContext db) : base(db)
    {
        _dbContext = db;
    }

    public void Update(OrderDetail orderDetail)
    {
        _dbContext.Update(orderDetail);
    }
}