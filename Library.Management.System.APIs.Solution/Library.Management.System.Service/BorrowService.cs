using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Repositories.Contract.IGenericRepository;
using Library.Management.System.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Library.Management.System.Service
{
    class BorrowService 
    {
        //private readonly IGenericRepository<BorrowedBook> _borrowedBookRepository;
        //private readonly IGenericRepository<Book> _bookRepository;
        //private readonly UserManager<ApplicationUser> _userManager;





        //// Constructor to initialize dependencies
        //public BorrowService(IGenericRepository<BorrowedBook> borrowedBookRepository
        //    , IGenericRepository<Book> bookRepository,
        //    UserManager<ApplicationUser> userManager)
        //{
        //    _borrowedBookRepository = borrowedBookRepository;
        //    _bookRepository = bookRepository;
        //    _userManager = userManager;
        
        //}



        //public Task<BorrowedBook> BorrowedBookByIdService(int bookId )
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IReadOnlyList<BorrowedBook>> GetAllBorrowedBooksService(int userid)
        //{
        //    //// Get the current user's ID from Token
           
        //    var userId = _userManager.FindByIdAsync(user.Id.ToString());
        //    if (string.IsNullOrEmpty(userId.ToString()));
        //        //return Unauthorized("User not authenticated.");
        //    var id = userId;
        //    //// Get all borrowed books for the user
        //    var borrowedBooks = await _borrowedBookRepository.GetAllAsync(id);
        //}
    }
}
