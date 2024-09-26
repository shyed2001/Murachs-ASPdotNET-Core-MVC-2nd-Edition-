namespace Bookstore.Models
{
    public class BookListViewModel 
    {
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
        public BookGridData CurrentRoute { get; set; } = new BookGridData();
        public int TotalPages { get; set; }

        // data for pagesize drop-down - hardcoded
        public int[] PageSizes => new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    }
}
