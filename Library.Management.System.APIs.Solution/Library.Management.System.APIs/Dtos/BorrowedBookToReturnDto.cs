namespace Library.Management.System.APIs.Dtos
{
    public class BorrowedBookToReturnDto
    {
        public DateTime BorrowedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }
        public string Username { get; set; }
        public string BookTitle { get; set; }
        public int BookId { get; set; }

    }
}
