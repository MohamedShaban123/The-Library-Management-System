using AutoMapper;
using Library.Management.System.APIs.Dtos;
using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Repositories.Contract.IGenericRepository;
using Library.Management.System.Core.UnitOfWorks.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Management.System.APIs.Controllers
{
    // Controller for managing borrowed books
    public class borrowController : BaseApiController
    {
        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;




        // Constructor to initialize dependencies
        public borrowController(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }






        //// Endpoint to get all borrowed books for the authenticated user
        [Authorize(Roles ="User")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BorrowedBookToReturnDto>>> GetAllBorrowedBooks()
        {
            //// Get the current user's ID from Token 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");
             var id = int.Parse(userId);
            //// Get all borrowed books for the user
            var borrowedBooks = await _unitOfWork.Repository<BorrowedBook>().GetAllAsync(id);
           var mappedBorrowedBook= _mapper.Map<IReadOnlyList<BorrowedBook>, IReadOnlyList<BorrowedBookToReturnDto>>(borrowedBooks);
            if (mappedBorrowedBook == null)
                return NotFound("Borrowed books not found");
            return Ok(mappedBorrowedBook);
        }





        //// Endpoint to borrow a book by its ID
        [Authorize(Roles = "User")]
        [HttpPost("{bookId}")]
        public async Task<ActionResult<BorrowedBookToReturnDto>> BorrowedBookById( BorrowedBookDto model,  int bookId )
        {
            // Set IsAvailable=True if ReturnDate Expired
            var borrowedBooks = await _unitOfWork.Repository<BorrowedBook>().GetAllAsync();
            var lastBorrowedBook = borrowedBooks.OrderByDescending(BB => BB.ReturnDate).FirstOrDefault();

            // Get the  Book based on id
            var existingbook = await _unitOfWork.Repository<Book>().GetAsync(bookId);
            // Check if the book exists and is available
            if (existingbook == null)
                return Ok("Book not Found");
            // Check IsAvailable=True if ReturnDate Expired
            if (lastBorrowedBook == null || lastBorrowedBook.ReturnDate <= DateTime.Now)
                existingbook.IsAvailable = true;
                // Check if the book   is available
            if (existingbook.IsAvailable == false)
            return Ok("Book not available");
            
          
             


            // Get the current user's ID from the token
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("This User UnAuthorized");


           // Get previous BorrowedBooks to check if this user already borrow this book before or not
            var previousBorrowedBooksById = await _unitOfWork.Repository<BorrowedBook>().GetAllAsync(int.Parse(userId),bookId);
            var previousBorrowedBooksByIdValue = previousBorrowedBooksById.FirstOrDefault();
            if (previousBorrowedBooksByIdValue is not null)
                return BadRequest("The same book cannot be borrowed twice");



            // Check if the return date is valid (not more than 14 days from the borrowed date)
            if ((model.ReturnDate - model.BorrowDate).TotalDays > 14)
            {
                return BadRequest("The return date must be within 14 days from the borrowed date.");
            }


            //// Create a new borrowed book record
            var borrowedBook = new BorrowedBook
            {
                BorrowedDate=model.BorrowDate,
                ReturnDate = model.ReturnDate,
                BookId = existingbook.Id,
                UserId = int.Parse(userId)
            };

           
            //// Mark the book as not available
            existingbook.IsAvailable = false;

            // Update the book and add the borrowed book record
            await _unitOfWork.Repository<BorrowedBook>().AddAsync(borrowedBook);


         

            await _unitOfWork.CompleteAsync();

            return Ok(new BorrowedBookToReturnDto
            {
                BorrowedDate=borrowedBook.BorrowedDate,
                ReturnDate = borrowedBook.ReturnDate,
                BookTitle = borrowedBook.Book.Title,
                BookId   = borrowedBook.Book.Id
            });


        }





    }
}
