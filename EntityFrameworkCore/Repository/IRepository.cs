using EntityFrameworkCore;

namespace EntityFrameworkCore.Repository
{
    public interface IRepository<TId, TEntity> where TEntity : class, new()
    {
        ApplicationDbContext Context { get; }
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetAsync(TId id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TId id);
    }
}
