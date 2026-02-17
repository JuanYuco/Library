using Library.Application.DTOs.Common;

namespace Library.Application.DTOs.Author
{
    public class AuthorCollectionResponseDTO : ResponseBase
    {
        public List<AuthorDTO> Authors { get; set; } = new List<AuthorDTO>();
    }
}
