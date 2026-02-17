using Library.Web.Models.DTOs.Author;

namespace Library.Web.Models.ViewModels.Author
{
    public class IndexViewModel
    {
        public List<AuthorDTO> Authors { get; set; } = new List<AuthorDTO>();
    }
}
