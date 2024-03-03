using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Setsis.Core.Models;
using Setsis.Core.Repositories;
using Setsis.Core.UnitOfWork;
using Setsis.Infrastructure.Repositories;

namespace Setsis.Infrastructure.UnitOfWork
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly TDbContext _dbContext;

        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommmitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
