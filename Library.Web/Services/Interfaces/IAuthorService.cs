using Library.Web.Models.DTOs.Author;

namespace Library.Web.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorCollectionResponseDTO> GetAllAsync();
        Task<AuthorMinfiedCollectionResponseDTO> GetAllMinifiedAsync();
        Task<AuthorDeleteResponseDTO> DeleteAsync(int id);
        Task<AuthorSaveResponseDTO> CreateAsync(AuthorDTO authorDTO);
    }
}
