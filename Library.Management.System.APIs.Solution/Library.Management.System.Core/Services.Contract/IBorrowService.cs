using Library.Management.System.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Core.Services.Contract
{
  public  interface IBorrowService
    {
        Task<IReadOnlyList<BorrowedBook>> GetAllBorrowedBooksService();
        Task<BorrowedBook> BorrowedBookByIdService(int bookId);


    }
}
