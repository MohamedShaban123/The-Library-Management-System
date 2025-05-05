using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Params;
using Library.Management.System.Core.Specifications.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Library.Management.System.Core.Repositories.Contract.IGenericRepository
{
    // Generic repository interface for CRUD operations
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // Get all entities with optional filtering, sorting, and pagination
        Task<IReadOnlyList<T>> GetAllAsync(BookParams bookParams);
        // Get all entities by a specific ID (e.g., user ID for borrowed books)
        Task<IReadOnlyList<T>> GetAllAsync(int userId , int bookId  );
        Task<IReadOnlyList<T>> GetAllAsync(int userId);
        Task<IReadOnlyList<T>> GetAllAsync();
        // Get a single entity by its ID
        Task<T?> GetAsync(int id);
        // Add a new entity
        Task AddAsync(T Entity);
        // Delete an existing entity
        Task DeleteAsync(T Entity);
        // Update an existing entity
        Task UpdateAsync(T Entity);
        // Get the total count of entities
        Task<int> GetCountAsync();


        /*========================= Dynamic  Query Using Specification Design Pattern=====================*/
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        Task<T?> GetWithSpecAsync(ISpecifications<T> spec);



    }
}
