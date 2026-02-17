using Library.Application.DTOs.Common;

namespace Library.Application.DTOs.Book
{
    public class BookCollectionResponseDTO : ResponseBase
    {
        public List<BookInformationDTO> Books { get; set; } = new List<BookInformationDTO>();
    }
}
