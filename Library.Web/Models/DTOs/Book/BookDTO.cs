using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.DTOs.Book
{
    public class BookDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El año es obligatorio")]
        public int Year { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, ErrorMessage = "Debe contener hasta 200 carácteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, ErrorMessage = "Debe contener hasta 200 carácteres.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "El número de páginas es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de páginas es invalido")]
        public int PagesNumber { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio")]
        public int AuthorId { get; set; }
    }
}
