using System.Dynamic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CMS.DAL;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepo(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }


    //> GetAllAsync(query, p => p.app, p => p.doctor, p => p.dfdf, ...)
    public async Task<IEnumerable<T>> GetAllAsync(Query query, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> data = _dbSet.AsNoTracking();

        if (includes.Count() > 0)
        {
            foreach (var inc in includes)
            {
                data = data.Include(inc);
            }
        }

        // 🔍 Search (Name)
        if (!string.IsNullOrEmpty(query.SearchTerm))
        {
            data = data.Where(e =>
                EF.Property<string>(e, "Name").Contains(query.SearchTerm));
        }

        // 📄 Pagination
        var skip = (query.PageNumber - 1) * query.PageSize;

        return await data
            .Skip(skip)
            .Take(query.PageSize)
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        //> model as no tracking
        IQueryable<T> query = _dbSet.AsNoTracking();


        if (includes != null && includes.Any())
        {
            foreach (var inc in includes)
            {
                query = query.Include(inc);
            }
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }


    public async Task AddAsync(T entity)
        => await _dbSet.AddAsync(entity);

    public void Update(T entity)
        => _dbSet.Update(entity);

    public void Delete(T entity)
        => _dbSet.Remove(entity);
}
