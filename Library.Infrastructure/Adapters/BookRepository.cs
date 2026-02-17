using Library.Domain.Enitities;
using Library.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Adapters
{
    public class BookRepository(AppDBContext dbContext) : IBookRepository
    {
        public async Task<Book> CreateAsync(Book book)
        {
            var entryBook = await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            return entryBook.Entity;
        }

        public async Task DeleteAsync(Book book)
        {
            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await dbContext.Books.Include(x => x.Author).ToListAsync();
        }

        public async Task<List<Book>> GetByAuthorIdAsync(int authorId)
        {
            return await dbContext.Books.Where(x => x.AuthorId == authorId).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await dbContext.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
