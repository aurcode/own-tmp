using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCore;

namespace EntityFrameworkCore.Repository
{
    public class Repository<TId, TEntity> : IRepository<TId, TEntity> where TEntity : class, new()
    {
        private readonly ApplicationDbContext _context;
        public ApplicationDbContext Context { get => _context; }
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {



            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                var result2 = await _context.AddAsync(entity);

                await _context.SaveChangesAsync();

                return result2.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public virtual async Task DeleteAsync(TId id)
        {
            var entity = await _context.FindAsync<TEntity>(id);
            _context.Remove<TEntity>(entity);
            await _context.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            try
            {
                return _context.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual async Task<TEntity> GetAsync(TId id)
        {
            var entity = await _context.FindAsync<TEntity>(id);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
    }
}
