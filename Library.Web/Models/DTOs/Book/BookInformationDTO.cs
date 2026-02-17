namespace Library.Web.Models.DTOs.Book
{
    public class BookInformationDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public int PagesNumber { get; set; }
        public string Author { get; set; }
    }
}
