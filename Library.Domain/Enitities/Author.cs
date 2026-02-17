namespace Library.Domain.Enitities
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BornCity { get; set; }
        public string Email { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
