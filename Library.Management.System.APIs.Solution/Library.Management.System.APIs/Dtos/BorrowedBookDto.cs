namespace Library.Management.System.APIs.Dtos
{
    public class BorrowedBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime BorrowDate { get; set; } 
        public DateTime ReturnDate { get; set; }
    }
}
