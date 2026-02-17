using Library.Application.DTOs.Common;

namespace Library.Application.DTOs.Author
{
    public class AuthorMinifiedCollectionResponseDTO : ResponseBase
    {
        public List<AuthorMinifiedDTO> Authors { get; set; } = new List<AuthorMinifiedDTO>();
    }
}
