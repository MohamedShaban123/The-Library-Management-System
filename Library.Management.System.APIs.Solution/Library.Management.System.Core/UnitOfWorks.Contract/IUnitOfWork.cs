using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Repositories.Contract.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Core.UnitOfWorks.Contract
{
   public interface IUnitOfWork : IAsyncDisposable 
     {
        Task<int> CompleteAsync();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    }
}
