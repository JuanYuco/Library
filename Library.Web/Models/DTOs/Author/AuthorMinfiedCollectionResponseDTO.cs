using Library.Web.Models.DTOs.Common;

namespace Library.Web.Models.DTOs.Author
{
    public class AuthorMinfiedCollectionResponseDTO : ResponseBaseDTO
    {
        public List<AuthorMinifiedDTO> Authors { get; set; } = new List<AuthorMinifiedDTO>();
    }
}
