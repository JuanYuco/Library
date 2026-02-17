namespace Library.Domain.Enitities
{
    public class Book
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public int PagesNumber { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
