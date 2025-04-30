using Library.Management.System.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Repository.Data.Configurations
{
    // Configuration class for the BorrowedBook entity
    class BorrowedBookConfigurations : IEntityTypeConfiguration<BorrowedBook>
    {
        // Method to configure the BorrowedBook entity
        public void Configure(EntityTypeBuilder<BorrowedBook> builder)
        {
            // Configure the relationship between BorrowedBook and ApplicationUser
            builder.HasOne(borrowedbook => borrowedbook.User)
                   .WithMany()
                   .HasForeignKey(Fk=>Fk.UserId);

            // Configure the relationship between BorrowedBook and Book
            builder.HasOne(borrowedbook => borrowedbook.Book)
                   .WithMany()
                   .HasForeignKey(FK=>FK.BookId);

        }
    }
}
