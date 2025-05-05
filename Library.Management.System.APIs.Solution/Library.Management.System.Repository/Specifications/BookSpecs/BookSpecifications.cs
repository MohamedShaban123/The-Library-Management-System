using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Repository.Specifications.BookSpecs
{
  public  class BookSpecifications : BaseSpecifications<Book>
    {
        public BookSpecifications(BookParams bookParams) 
            :base(
                 // apply filtering
                 B=>  string.IsNullOrEmpty(bookParams.FilterByAuthor)  || B.Author.ToLower()==bookParams.FilterByAuthor.ToLower() &&
                     string.IsNullOrEmpty(bookParams.FilterByTitle) || B.Title.ToLower() == bookParams.FilterByTitle.ToLower()   
                 )
        {
           // apply includes
           // apply sorting
           if( !string.IsNullOrEmpty(bookParams.SortByTitle))
            {
                switch(bookParams.SortByTitle)
                {
                    case "SortByTitleAsc":
                        ApplyOrderAsc(B => B.Title);
                        break;
                    case "SortByTitleDesc":
                        ApplyOrderDesc(B => B.Title);
                        break;
                }

            }
            if (!string.IsNullOrEmpty(bookParams.SortByAuthor))
            {
                switch (bookParams.SortByAuthor)
                {
                    case "SortByAuthorAsc":
                        ApplyOrderAsc(B => B.Author);
                        break;
                    case "SortByAuthorDesc":
                        ApplyOrderDesc(B => B.Author);
                        break;
                }

            }

            if (!string.IsNullOrEmpty(bookParams.SortByPublishedYear))
            {
                switch (bookParams.SortByPublishedYear)
                {
                    case "SortByPublishedYearAsc":
                        ApplyOrderAsc(B => B.PublishedYear);
                        break;
                    case "SortByPublishedYearDesc":
                        ApplyOrderDesc(B => B.PublishedYear);
                        break;
                }

            }

            // Apply Pageination
            ApplyPagination(skip:(bookParams.PageIndex - 1)*bookParams.PageSize ,take:bookParams.PageSize);

        }

        public BookSpecifications(int id)
            :base(B=>B.Id == id)
        {
            
        }
    }
}
