using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Repositories.Contract.IGenericRepository;
using Library.Management.System.Core.UnitOfWorks.Contract;
using Library.Management.System.Repository.Data.Contexts;
using Library.Management.System.Repository.Repositories.GenericRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Repository.UnitOfWorks
{
  public  class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private Hashtable _repositories;



        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }



        public async Task<int> CompleteAsync()
           =>  await _dbContext.SaveChangesAsync();
       

        public async ValueTask DisposeAsync()
          => await _dbContext.DisposeAsync();



        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name; 
            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(key, repository);
            }

            return _repositories[key] as IGenericRepository<TEntity>;
        }

     
    }
}
