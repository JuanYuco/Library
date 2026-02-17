using Library.Application.DTOs.Author;

namespace Library.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorCollectionResponseDTO> GetAllAsync(AuthorCollectionRequestDTO requestDTO);
        Task<AuthorMinifiedCollectionResponseDTO> GetAllMinifiedAsync(AuthorMinifiedCollectionRequestDTO requestDTO);
        Task<AuthorSaveResponseDTO> CreateAsync(AuthorSaveRequestDTO request);
        Task<AuthorDeleteResponseDTO> DeleteAsync(AuthorDeleteRequestDTO request);
    }
}
