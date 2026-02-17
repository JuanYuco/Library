using Library.Domain.Enitities;

namespace Library.Domain.Ports
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<List<Book>> GetByAuthorIdAsync(int authorId);
        Task<Book?> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task DeleteAsync(Book book);
    }
}
