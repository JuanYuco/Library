using Library.Domain.Enitities;
using Library.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Adapters
{
    public class AuthorRepository(AppDBContext dbContext) : IAuthorRepository
    {
        public async Task<List<Author>> GetAllAsync()
        {
            return await dbContext.Authors.ToListAsync();
        }

        public async Task<Author?> GetByEmailAsync(string email)
        {
            return await dbContext.Authors.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await dbContext.Authors.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Author> CreateAsync(Author author)
        {
            var entryAuthor = await dbContext.Authors.AddAsync(author);
            await dbContext.SaveChangesAsync();

            return entryAuthor.Entity;
        }

        public async Task DeleteAsync(Author author)
        {
            dbContext.Authors.Remove(author);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Author>> GetAllMinifiedAsync()
        {
            return await dbContext.Authors.Select(x => new Author
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email
            }).ToListAsync();
        }
    }
}
