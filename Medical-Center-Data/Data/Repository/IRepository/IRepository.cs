namespace Medical_Center_Data.Data.Repository.IRepository
{
    public interface IRepo<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetOneAsync(int id, bool tracked = true);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
