namespace Bookstore.Models
{
    public interface IBookRepository : IRepository<Book>
    {
        void AddUpdateAuthors(Book book, int[] authorids, IRepository<Author> authorData);
    }
}
