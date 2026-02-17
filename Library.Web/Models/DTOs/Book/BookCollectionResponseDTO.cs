using Library.Web.Models.DTOs.Common;

namespace Library.Web.Models.DTOs.Book
{
    public class BookCollectionResponseDTO : ResponseBaseDTO
    {
        public List<BookInformationDTO> Books { get; set; } = new List<BookInformationDTO>();
    }
}
