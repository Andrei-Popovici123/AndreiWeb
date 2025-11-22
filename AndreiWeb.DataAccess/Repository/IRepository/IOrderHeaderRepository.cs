using AndreiWeb.Models;

namespace AndreiWeb.DataAccess.Repository.IRepository;

public interface IOrderHeaderRepository: IRepository<OrderHeader>
{
    void Update(OrderHeader orderHeader);
}