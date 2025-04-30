using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Library.Management.System.Core.Entities
{
  public  class BorrowedBook : BaseEntity
    {

        public DateTime BorrowedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }




        // Foreign keys for a relationship between Book Table and  IdentityUser (User)

        public int UserId { get; set; }
        public int BookId { get; set; }


        // Navigation property for User , Book Tables
        public Book Book { get; set; }
        public ApplicationUser User { get; set; }

    }
}
