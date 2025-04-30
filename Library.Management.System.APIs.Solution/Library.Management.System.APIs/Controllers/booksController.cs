using AutoMapper;
using Library.Management.System.APIs.Dtos;
using Library.Management.System.APIs.Helpers;
using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Params;
using Library.Management.System.Core.Repositories.Contract.IGenericRepository;
using Library.Management.System.Core.UnitOfWorks.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Management.System.APIs.Controllers
{  
    // Controller for managing books
    public class BooksController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MemoryCacheEntryOptions _cacheOptions;
        public IMemoryCache _cache { get; }




        // Constructor to initialize dependencies
        public BooksController( IUnitOfWork unitOfWork , IMapper mapper, IMemoryCache cache)
        {
           
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
            // Set cache expiration policy
            _cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15))  // Cache expires in 15 minutes
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));   // Refresh if accessed within 2 minute
        }


            



        //// Endpoint to get all books with optional filtering, sorting, and pagination
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<BookDto>>> GetALLBooks([FromQuery] BookParams bookParams)
        {
            try
            {
                int Count;
                string cacheKey = $"Books_{bookParams.PageIndex}_{bookParams.PageSize}";
                if (!_cache.TryGetValue(cacheKey, out IReadOnlyList<BookDto> mappedBooksIntoBooksDto))
                {
                    
                    var books = await _unitOfWork.Repository<Book>().GetAllAsync(bookParams);
                    if (books == null)
                        return NotFound(new { Message = "There is no any books" });
                    // Map books to BookDto
                    mappedBooksIntoBooksDto = _mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BookDto>>(books);
                    // Cache the result
                    _cache.Set(cacheKey, mappedBooksIntoBooksDto, _cacheOptions);
                }
                Count = await _unitOfWork.Repository<Book>().GetCountAsync();
                // Return paginated result
                return Ok(new Pagination<BookDto>(bookParams.PageIndex, bookParams.PageSize, Count, mappedBooksIntoBooksDto));
            }
            catch (Exception ex)
            {

                
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }



        // Endpoint to get a book by its ID
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _unitOfWork.Repository<Book>().GetAsync(id);
            if (book == null)
                return NotFound(new { Message = $"There is no any book with this id {id}" });
            // Map book to BookDto
            var mappedBookIntoBookDto = _mapper.Map<Book, BookDto>(book);
            return Ok(mappedBookIntoBookDto);
        }





        //// Endpoint to add a new book, accessible only by Admins
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<int>> AddBook(BookDto book)
        {
            // Map BookDto to Book
            var mappedBookDtoIntoBook = _mapper.Map<BookDto, Book>(book);
             await _unitOfWork.Repository<Book>().AddAsync(mappedBookDtoIntoBook);
            var NumOfRowsAffected = await _unitOfWork.CompleteAsync();
            if (NumOfRowsAffected == 0)
                return NotFound();

            return Ok(NumOfRowsAffected);
        }





        //// Endpoint to update an existing book, accessible only by Admins
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<int>> UpdateBook(int id, BookDto book)
        {
            // Map BookDto to Book
            var mappedBookDtoIntoBook = _mapper.Map<BookDto, Book>(book);
            var existingbook = await GetBookById(id);
            if (existingbook == null)
                return NotFound();
           
            await _unitOfWork.Repository<Book>().UpdateAsync(mappedBookDtoIntoBook);
            var NumOfRowsAffected= await _unitOfWork.CompleteAsync();
            return (NumOfRowsAffected);

        }





        //// Endpoint to delete a book, accessible only by Admins
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteBook(int id, BookDto book)
        {
            // Map BookDto to Book
            var mappedBookDtoIntoBook = _mapper.Map<BookDto, Book>(book);
            var existingbook = await GetBookById(id);
            if (existingbook == null)
                return NotFound();
              await _unitOfWork.Repository<Book>().DeleteAsync(mappedBookDtoIntoBook);
            var NumOfRowsAffected= await _unitOfWork.CompleteAsync();
            return (NumOfRowsAffected);
        }


    }
}
