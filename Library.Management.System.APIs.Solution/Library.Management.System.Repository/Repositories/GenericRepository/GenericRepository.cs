using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Repositories.Contract.IGenericRepository;
using Library.Management.System.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Management.System.Core.Params;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Net;
using Library.Management.System.Core.Specifications.Contract;
using Library.Management.System.Repository.Specifications;




namespace Library.Management.System.Repository.Repositories.GenericRepository
{
    // Generic repository implementation for CRUD operations
    public class GenericRepository<T> : IGenericRepository<T>  where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        private string? ResultFilterByTitle { get; set; }
        private string? ResultFilterByAuthor { get; set; }

        // Constructor to initialize the repository with the database context
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //// Get all entities with optional filtering, sorting, and pagination
        public async Task<IReadOnlyList<T>> GetAllAsync(BookParams bookParams)
        {
            if (typeof(T) == typeof(Book))
            {
                ResultFilterByTitle = bookParams.FilterByTitle;
                ResultFilterByAuthor = bookParams.FilterByAuthor;

                var query = _dbContext.Set<Book>().AsQueryable();


                // Filtering 
                if (!string.IsNullOrEmpty(bookParams.FilterByTitle))
                    query = query.Where(B => B.Title.Contains(bookParams.FilterByTitle));
                if (!string.IsNullOrEmpty(bookParams.FilterByAuthor))
                    query = query.Where(B => B.Author.Contains(bookParams.FilterByAuthor));



                // Sorting 
                if (!string.IsNullOrEmpty(bookParams.SortByTitle))
                {
                    switch (bookParams.SortByTitle)
                    {
                        case "titilAsc":
                            query = query.OrderBy(B => B.Title);
                            break;
                        case "titilDesc":
                            query = query.OrderByDescending(B => B.Title);
                            break;
                        default:
                            query = query.OrderBy(B => B.Title);
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(bookParams.SortByAuthor))
                {
                    switch (bookParams.SortByAuthor)
                    {
                        case "authorAsc":
                            query = query.OrderBy(B => B.Author);
                            break;
                        case "authorDesc":
                            query = query.OrderByDescending(B => B.Author);
                            break;
                        default:
                            query = query.OrderBy(B => B.Author);
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(bookParams.SortByPublishedYear))
                {
                    switch (bookParams.SortByPublishedYear)
                    {
                        case "publishedYearAsc":
                            query = query.OrderBy(B => B.PublishedYear);
                            break;
                        case "publishedYearDesc":
                            query = query.OrderByDescending(B => B.PublishedYear);
                            break;
                        default:
                            query = query.OrderBy(B => B.PublishedYear);
                            break;
                    }
                }


                // Pagination
                query = query.Skip((bookParams.PageIndex - 1) * bookParams.PageSize).Take(bookParams.PageSize);


                return (IReadOnlyList<T>)await query.ToListAsync();
            }



            return await _dbContext.Set<T>().ToListAsync();
        }









        //// Get a single entity by its ID
        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);   
        }

        // Add a new entity
        public async Task AddAsync(T Entity)
         =>await  _dbContext.Set<T>().AddAsync(Entity);
       

        // Delete an existing entity
        public async Task DeleteAsync(T Entity)
        {
            _dbContext.Set<T>().Remove(Entity);
        }



        // Update an existing entity
        public async Task UpdateAsync(T Entity)
        {
             _dbContext.Set<T>().Update(Entity);
        }



        // Get all entities by a specific ID (e.g., user ID for borrowed books)
        public async Task<IReadOnlyList<T>> GetAllAsync(int userId ,int bookId )
        {
            if (typeof(T) == typeof(BorrowedBook))
              return  (IReadOnlyList<T>) await _dbContext.Set<BorrowedBook>().Include(B => B.Book)
                                                         .Where(  B => B.UserId == userId  && B.BookId == bookId )
                                                         .ToListAsync();
            return  await _dbContext.Set<T>().ToListAsync();
        }


        // Get the total count of entities
        public async Task<int> GetCountAsync()
        {
            var count =0;

            if (typeof(T) == typeof(Book))
            {
                var query = _dbContext.Set<Book>().AsQueryable().AsNoTracking();
                // Filtering 
                if (!string.IsNullOrEmpty(ResultFilterByTitle))
                    query = query.Where(B => B.Title.ToLower().Contains(ResultFilterByTitle.ToLower()));
                if (!string.IsNullOrEmpty(ResultFilterByAuthor))
                    query = query.Where(B => B.Author.ToLower().Contains(ResultFilterByAuthor.ToLower()));
                    count = await query.CountAsync();
              
               

            }

            return  count;

        }

        public async Task<IReadOnlyList<T>> GetAllAsync(int userId)
        {
            if (typeof(T) == typeof(BorrowedBook))
            {
                var result = await _dbContext.Set<BorrowedBook>()
                    .Include(b => b.Book)
                    .Where(b => b.UserId == userId)
                    .ToListAsync();
                return result.Cast<T>().ToList();
            }

            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }






        /*========================= Dynamic  Query Using Specification Design Pattern=====================*/
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await BulidQuery<T>.GetQuery(_dbContext.Set<T>(), spec).ToListAsync();
        }

        public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        { 
            return await BulidQuery<T>.GetQuery(_dbContext.Set<T>(), spec).FirstOrDefaultAsync();
        }



    }
}
