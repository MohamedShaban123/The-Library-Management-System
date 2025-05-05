
using Library.Management.System.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Repository.Data.Contexts
{
    // Represents the application's database context, including identity and custom entities

    public class AppDbContext  : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    {
        // Constructor to initialize the DbContext with options

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        // Override the OnModelCreating method to configure the model

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply configurations from the current assembly
            // This will automatically apply configurations defined in the Configurations folder
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        // DbSet representing the Books table
        public DbSet<Book> Books { get; set; }
        // DbSet representing the BorrowedBooks table
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }

        
    }
}
