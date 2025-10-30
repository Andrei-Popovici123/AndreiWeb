using System.Linq.Expressions;
using AndreiWeb.DataAccess.Data;
using AndreiWeb.DataAccess.Repository.IRepository;
using AndreiWeb.Models;

namespace AndreiWeb.DataAccess.Repository;

public class CompanyRepository :  Repository<Company>, ICompanyRepository
{
    private ApplicationDbContext _db;

    public CompanyRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Company company)
    {
        _db.Companies.Update(company);
    }


}