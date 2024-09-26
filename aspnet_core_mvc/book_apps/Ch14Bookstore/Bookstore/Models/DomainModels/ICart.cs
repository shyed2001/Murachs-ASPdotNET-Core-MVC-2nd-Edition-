namespace Bookstore.Models
{
    public interface ICart
    {
        int? Count { get; }
        IEnumerable<CartItem> List { get; }
        double Subtotal { get; }

        void Add(CartItem item);
        void Clear();
        void Edit(CartItem item);
        CartItem? GetById(int? id);
        void Load(IRepository<Book> data);
        void Remove(CartItem item);
        void Save();
    }
}