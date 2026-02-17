using Library.Domain.Enitities;

namespace Library.Domain.Ports
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<List<Author>> GetAllMinifiedAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author?> GetByEmailAsync(string email);
        Task<Author> CreateAsync(Author author);
        Task DeleteAsync(Author author);
    }
}
