namespace Library.Application.DTOs.Author
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BornCity { get; set; }
        public string Email { get; set; }
    }
}
