using Library.Web.Models.DTOs.Book;

namespace Library.Web.Models.ViewModels.Book
{
    public class IndexViewModel
    {
        public List<BookInformationDTO> Books { get; set; } = new List<BookInformationDTO>();
    }
}
