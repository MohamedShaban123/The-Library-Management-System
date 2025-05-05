using AutoMapper;
using Library.Management.System.APIs.Dtos;
using Library.Management.System.Core.Entities;

namespace Library.Management.System.APIs.Helpers
{
    // AutoMapper profile for configuring object-object mappings
    public class MappingProfile : Profile
    {
        // Constructor to define the mappings
        public MappingProfile()
        {
            // Create a mapping between Book and BookDto, and enable reverse mapping
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BorrowedBook, BorrowedBookToReturnDto>()
                    .ForMember(destination=>destination.Username,options=>options.MapFrom(source=>source.User.UserName))
                    .ForMember(destination=>destination.BookTitle,options=>options.MapFrom(source=>source.Book.Title))
                    .ForMember(destination=>destination.BookId,options=>options.MapFrom(source=>source.Book.Id));
        }
    }
}
