using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Core.Entities
{
    // Represents a book entity in the library management system
    public class Book : BaseEntity
    {
        // The title of the book
        public string Title { get; set; }
        // The author of the book
        public string Author { get; set; }
        // The genre of the book
        public string Genre { get; set; }
        // The year the book was published
        public int PublishedYear { get; set; }
        // Indicates whether the book is available for borrowing
        public bool IsAvailable { get; set; } = true;

        

    }
}
