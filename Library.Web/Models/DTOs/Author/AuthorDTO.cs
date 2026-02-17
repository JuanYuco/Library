using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.DTOs.Author
{
    public class AuthorDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(200, ErrorMessage = "El nombre debe contener hasta 200 carácteres.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "La ciudad natal es obligatoria")]
        [StringLength(200, ErrorMessage = "Debe contener hasta 200 carácteres.")]
        public string BornCity { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [StringLength(200, ErrorMessage = "Debe contener hasta 200 carácteres.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El email es ínvalido")]
        public string Email { get; set; }
    }
}
