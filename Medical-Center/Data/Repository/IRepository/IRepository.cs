using System.Linq.Expressions;

namespace Medical_Center.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T GetOne(Expression<Func<T, bool>> filter = null, bool tracked = true);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
        Task SaveAsync();
    }
}
