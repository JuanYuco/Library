using Library.Application.DTOs.Book;

namespace Library.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookCollectionResponseDTO> GetAllAsync(BookCollectionRequestDTO request);
        Task<BookSaveResponseDTO> CreateAsync(BookSaveRequestDTO request);
        Task<BookDeleteResponseDTO> DeleteAsync(BookDeleteRequestDTO request);
    }
}
