using Microsoft.EntityFrameworkCore;
using Setsis.Core.Repositories;
using System.Linq.Expressions;

namespace Setsis.Infrastructure.Repositories
{
    public class Repository<Tentity> : IRepository<Tentity> where Tentity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<Tentity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Tentity>();
        }

        public IQueryable<Tentity> Entities => _dbSet;

        public async Task AddAsync(Tentity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<Tentity>> GetAllAsync()
        {
            var entries = await _dbSet.ToListAsync();

            if (entries != null)
                _dbContext.Entry(entries).State = EntityState.Detached;

            return await _dbSet.ToListAsync();
        }

        public async Task<Tentity?> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
                _dbContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public void Remove(Tentity entity)
        {
            _dbSet.Remove(entity);
        }

        public Tentity Update(Tentity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public IQueryable<Tentity> Where(Expression<Func<Tentity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
