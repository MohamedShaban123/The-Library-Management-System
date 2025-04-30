namespace Library.Management.System.APIs.Helpers
{
    // Generic class for handling pagination of data
    public class Pagination<T>
    {
        // The current page index
        public int PageIndex { get; set; }
        // The size of each page
        public int PageSize  { get; set; }
        // The total count of items
        public int Count { get; set; }
        // The data for the current page
        public IReadOnlyList<T> Data { get; set; }
        // Constructor to initialize pagination properties
        public Pagination(int pageIndex, int pageSize,int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Count = count;
        }
    }
}
