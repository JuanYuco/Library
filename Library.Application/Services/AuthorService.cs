using Library.Application.DTOs.Author;
using Library.Application.Interfaces;
using Library.Domain.Ports;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Services
{
    public class AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository) : IAuthorService
    {
        public async Task<AuthorSaveResponseDTO> CreateAsync(AuthorSaveRequestDTO request)
        {
            var response = new AuthorSaveResponseDTO() { Successful = false };

            try
            {
                var validationMessage = AuthorValidation(request);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    response.UserMessage = validationMessage;
                    response.HttpCode = 400;
                    return response;
                }

                var authorByEmail = await authorRepository.GetByEmailAsync(request.Email);
                if (authorByEmail != null)
                {
                    response.UserMessage = "Ya existe un autor con el correo enviado.";
                    response.HttpCode = 400;
                    return response;
                }

                var author = await authorRepository.CreateAsync(new Domain.Enitities.Author
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    BornCity = request.BornCity,
                    BirthDate = request.BirthDate
                });

                response.UserMessage = "El autor se creó correctamente.";
                response.Successful = true;
            } catch (Exception ex)
            {
                response.UserMessage = "Ocurrió un error al intentar crear el autor.";
                response.InternalErrorMessage = ex.Message;
                response.HttpCode = 500;
            }

            return response;
        }

        private string AuthorValidation(AuthorDTO author)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrWhiteSpace(author.FullName) || author.FullName.Length > 200)
            {
                errors.Add("El nombre es obligatorio y debe tener menos de 200 carácteres.");
            }

            if (author.BirthDate > DateTime.Now)
            {
                errors.Add("Debe registrar una fecha valida");
            }

            if (string.IsNullOrWhiteSpace(author.BornCity) || author.BornCity.Length > 200)
            {
                errors.Add("La ciudad de nacimiento es obligatoria y debe tener menos de 200 carácteres.");
            }

            if (string.IsNullOrWhiteSpace(author.Email) || author.Email.Length > 200)
            {
                errors.Add("El correo eléctronico es obligatorio y debe tener menos de 200 carácteres.");
            }

            var validador = new EmailAddressAttribute();
            if (!validador.IsValid(author.Email))
            {
                errors.Add("El correo eléctronico no es válido.");
            }

            string message = "";
            if (errors.Count > 0)
            {
                message = string.Join(", ", errors);
            }

            return message;
        }

        public async Task<AuthorDeleteResponseDTO> DeleteAsync(AuthorDeleteRequestDTO request)
        {
            var response = new AuthorDeleteResponseDTO() { Successful = false };

            try
            {
                if (request.AuthorId <= 0)
                {
                    response.UserMessage = "El id del autor no es válido.";
                    response.HttpCode = 400;
                    return response;
                }

                var author = await authorRepository.GetByIdAsync(request.AuthorId);
                if (author == null)
                {
                    response.UserMessage = "El autor no existe.";
                    response.HttpCode = 400;
                    return response;
                }

                var authorBooks = await bookRepository.GetByAuthorIdAsync(request.AuthorId);
                if (authorBooks.Count > 0)
                {
                    response.UserMessage = "El autor no se puede eliminar ya que tiene libros relacionados.";
                    response.HttpCode = 409;
                    return response;
                }

                await authorRepository.DeleteAsync(author);

                response.UserMessage = "Se eliminó correctamente el autor.";
                response.Successful = true;
            } catch (Exception ex)
            {
                response.HttpCode = 500;
                response.UserMessage = "Ocurrió un error al intentar eliminar el author.";
                response.InternalErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<AuthorCollectionResponseDTO> GetAllAsync(AuthorCollectionRequestDTO requestDTO)
        {
            var response = new AuthorCollectionResponseDTO() { Successful = false };

            try
            {
                var authors = await authorRepository.GetAllAsync();
                foreach (var author in authors)
                {
                    response.Authors.Add(new AuthorDTO
                    {
                        Id = author.Id,
                        FullName = author.FullName,
                        Email = author.Email,
                        BirthDate = author.BirthDate,
                        BornCity = author.BornCity
                    });
                }

                response.Successful = true;
            } catch (Exception ex)
            {
                response.HttpCode = 500;
                response.UserMessage = "Ocurrió un error al consultar los autores, por favor intente más tarde.";
                response.InternalErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<AuthorMinifiedCollectionResponseDTO> GetAllMinifiedAsync(AuthorMinifiedCollectionRequestDTO requestDTO)
        {
            var response = new AuthorMinifiedCollectionResponseDTO() { Successful = false };

            try
            {
                var authors = await authorRepository.GetAllMinifiedAsync();
                foreach (var author in authors)
                {
                    response.Authors.Add(new AuthorMinifiedDTO
                    {
                        Id = author.Id,
                        Description = $"{author.FullName} ({author.Email})"
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
