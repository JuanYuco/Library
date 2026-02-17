using Library.Web.Models.DTOs.Common;

namespace Library.Web.Models.DTOs.Author
{
    public class AuthorCollectionResponseDTO : ResponseBaseDTO
    {
        public List<AuthorDTO> Authors { get; set; } = new List<AuthorDTO>();
    }
}
