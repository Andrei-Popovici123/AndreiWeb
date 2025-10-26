using System.Linq.Expressions;
using AndreiWeb.DataAccess.Data;
using AndreiWeb.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AndreiWeb.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;

    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>(); // this set is getting mapped on the model that you are working on, it is a generic implementation of all of the 
        //Crud ops
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }
}