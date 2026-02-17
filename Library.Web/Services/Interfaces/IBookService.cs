using Library.Web.Models.DTOs.Book;

namespace Library.Web.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookCollectionResponseDTO> GetAllAsync();
        Task<BookDeleteResponseDTO> DeleteAsync(int id);
        Task<BookSaveResponseDTO> CreateAsync(BookDTO book);
    }
}
