using System.Linq.Expressions;

namespace CMS.DAL;

public interface IGenericRepo<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(
        Query query,
        params Expression<Func<T, object>>[] includes);

    Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);

    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}

