using AndreiWeb.Models;

namespace AndreiWeb.DataAccess.Repository.IRepository;

public interface ICompanyRepository : IRepository<Company>
{
    void Update(Company company);
    
}