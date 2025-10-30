using AndreiWeb.DataAccess.Data;
using AndreiWeb.DataAccess.Repository.IRepository;

namespace AndreiWeb.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _db;
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }
    public ICompanyRepository Company{ get; private set; }
    
    public IShoppingCartRepository ShoppingCart { get; private set; }
    public IApplicationUserRepository ApplicationUser { get; }

    public UnitOfWork(ApplicationDbContext db) 
    {
        _db = db;
        ShoppingCart = new ShoppingCartRepository(_db);
        ApplicationUser = new ApplicationUserRepository(_db);
        Company = new CompanyRepository(_db);
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
    }
    public void Save()
    {
        _db.SaveChanges();
    }
}