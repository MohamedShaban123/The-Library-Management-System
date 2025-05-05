using Library.Management.System.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.Management.System.Repository.Data.Contexts
{
    // Static class for seeding initial data into the database
    public static class DataSeedContext
    {
        // Method to seed data asynchronously
        public static async Task SeedAsync(AppDbContext _dbContext)
        {
            // Define the base path for the data files
            var basePath = Path.Combine(AppContext.BaseDirectory, @"../../../../Library.Management.System.Repository");
            // Seed books data if the Books table is empty
            if (_dbContext.Set<Book>().Count() == 0)
            {
                // Read books data from JSON file
                var readBooksData = File.ReadAllText($"{basePath}/Data/DataSeeding/books.json");
                var books = JsonSerializer.Deserialize<List<Book>>(readBooksData);
                if(books?.Count() > 0)
                { 
                    // Add each book to the database context
                    foreach (var book in books)
                    {
                        _dbContext.Set<Book>().Add(book);
                    }
                    // Save changes to the database
                    await _dbContext.SaveChangesAsync();
                }
            }
            // Seed borrowed books data if the BorrowedBooks table is empty

            if (_dbContext.Set<BorrowedBook>().Count() == 0)
            {
                // Read borrowed books data from JSON file

                var readBorrowedBooksData = File.ReadAllText($"{basePath}/Data/DataSeeding/BorrowedBooks.json");
                var borrowedBooks = JsonSerializer.Deserialize<List<BorrowedBook>>(readBorrowedBooksData);
                if (borrowedBooks?.Count() > 0)
                {
                    // Add each borrowed book to the database context

                    foreach (var borrowedBook in borrowedBooks)
                    {
                        _dbContext.Set<BorrowedBook>().Add(borrowedBook);
                    }
                    // Save changes to the database
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
