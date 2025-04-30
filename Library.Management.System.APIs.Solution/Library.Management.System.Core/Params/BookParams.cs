using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Core.Params
{
  public  class BookParams
    {

        // Represents the parameters for filtering, sorting, and paginating books
        public string? FilterByTitle { get; set; }
        public string? FilterByAuthor { get; set; }
        public string? SortByTitle { get; set; }
        public string? SortByAuthor { get; set; }
        public string? SortByPublishedYear { get; set; }

        private const int MaxPageSize = 10;
        private int pageSize=5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;

      
       
    }
}
