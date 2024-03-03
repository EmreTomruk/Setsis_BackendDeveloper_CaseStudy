using Microsoft.EntityFrameworkCore;
using Setsis.Core.Repositories;

namespace Setsis.Core.UnitOfWork
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext, IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;

        Task CommmitAsync();

        void Commit();
    }
}
