using Library.Application.DTOs.Book;
using Library.Application.Interfaces;
using Library.Application.Settings;
using Library.Domain.Ports;
using Microsoft.Extensions.Options;

namespace Library.Application.Services
{
    public class BookService(IBookRepository bookRepository
        , IAuthorRepository authorRepository
        , IOptions<LibrarySetting> settings) : IBookService
    {
        public async Task<BookSaveResponseDTO> CreateAsync(BookSaveRequestDTO request)
        {
            var response = new BookSaveResponseDTO() { Successful = false };

            try
            {
                var validationMessage = BookValidation(request);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    response.UserMessage = validationMessage;
                    response.HttpCode = 400;
                    return response;
                }

                if (settings.Value.MaxBooks > 0)
                {
                    var books = await bookRepository.GetAllAsync();
                    if (books.Count >= settings.Value.MaxBooks)
                    {
                        response.UserMessage = "Se ha alcanzado el límite de libros creados.";
                        response.HttpCode = 409;
                        return response;
                    }
                }

                var author = await authorRepository.GetByIdAsync(request.AuthorId);
                if (author == null)
                {
                    response.UserMessage = "El autor asociado al libro no existe.";
                    response.HttpCode = 400;
                    return response;
                }

                var book = await bookRepository.CreateAsync(new Domain.Enitities.Book
                {
                    AuthorId = request.AuthorId,
                    Title = request.Title,
                    Gender = request.Gender,
                    PagesNumber = request.PagesNumber,
                    Year = request.Year
                });

                response.UserMessage = "El autor se creó correctamente.";
                response.Successful = true;
            }
            catch (Exception ex)
            {
                response.UserMessage = "Ocurrió un error al intentar crear el autor.";
                response.InternalErrorMessage = ex.Message;
                response.HttpCode = 500;
            }

            return response;
        }

        private string BookValidation(BookDTO book)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrWhiteSpace(book.Title) || book.Title.Length > 200)
            {
                errors.Add("El título es obligatorio y debe tener menos de 200 carácteres");
            }

            if (book.Year < 0 || book.Year > DateTime.Now.Year )
            {
                errors.Add("Debe registrar un año valida");
            }

            if (string.IsNullOrWhiteSpace(book.Gender) || book.Gender.Length > 200)
            {
                errors.Add("El género es obligatorio y debe tener menos de 200 carácteres");
            }

            if (book.PagesNumber <= 0)
            {
                errors.Add("El número de páginas debe ser mayor a 0");
            }

            if (book.AuthorId <= 0)
            {
                errors.Add("El id del autor es invalido");
            }

            string message = "";
            if (errors.Count > 0)
            {
                message = string.Join(", ", errors);
            }

            return message;
        }

        public async Task<BookDeleteResponseDTO> DeleteAsync(BookDeleteRequestDTO request)
        {
            var response = new BookDeleteResponseDTO() { Successful = false };

            try
            {
                if (request.Id <= 0)
                {
                    response.UserMessage = "El id del libro no es válido.";
                    response.HttpCode = 400;
                    return response;
                }

                var book = await bookRepository.GetByIdAsync(request.Id);
                if (book == null)
                {
                    response.UserMessage = "El libro que desea eliminar no existe.";
                    response.HttpCode = 400;
                    return response;
                }

                await bookRepository.DeleteAsync(book);

                response.UserMessage = "Se eliminó correctamente el libro.";
                response.Successful = true;
            }
            catch (Exception ex)
            {
                response.HttpCode = 500;
                response.UserMessage = "Ocurrió un error al intentar eliminar el libro.";
                response.InternalErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<BookCollectionResponseDTO> GetAllAsync(BookCollectionRequestDTO request)
        {
            var response = new BookCollectionResponseDTO() { Successful = false };

            try
            {
                var books = await bookRepository.GetAllAsync();
                foreach (var book in books)
                {
                    response.Books.Add(new BookInformationDTO
                    {
                        Id = book.Id,
                        Year = book.Year,
                        Author = book.Author.FullName,
                        Gender = book.Gender,
                        PagesNumber = book.PagesNumber,
                        Title = book.Title
                    });
                }

                response.Successful = true;
            }
            catch (Exception ex)
            {
                response.HttpCode = 500;
                response.UserMessage = "Ocurrió un error al consultar los autores, por favor intente más tarde.";
                response.InternalErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
