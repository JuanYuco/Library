using Library.Application.DTOs.Book;
using Library.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController(IBookService bookService) : Controller
    {
        /// <summary>
        /// It gets all books created.
        /// </summary>
        /// <returns>It returns a books list.</returns>
        /// <response code="200">Successful result</response>
        /// <response code="500">Database error</response>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await bookService.GetAllAsync(new Application.DTOs.Book.BookCollectionRequestDTO());
            if (!response.Successful)
            {
                return StatusCode(response.HttpCode, new { response.UserMessage });
            }

            return Ok(response.Books);
        }

        /// <summary>
        /// It creates the book on database.
        /// </summary>
        /// <param name="request">Book's information to save</param>
        /// <returns>It returns a message with the result information</returns>
        /// <response code="200">Created successfully</response>
        /// <response code="400">There are invalid properties or the limit has been reached</response>
        /// <response code="500">Database error</response>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(BookSaveRequestDTO request)
        {
            var response = await bookService.CreateAsync(request);
            if (!response.Successful)
            {
                return StatusCode(response.HttpCode, new { response.UserMessage });
            }

            return Ok(new { response.UserMessage });
        }

        /// <summary>
        /// It deletes the book from database.
        /// </summary>
        /// <param name="id">Book id</param>
        /// <returns>It returns a message with the result information</returns>
        /// <response code="200">Deleted successfully</response>
        /// <response code="400">The id is invalid or the book doesn't exists</response>
        /// <response code="500">Database error</response>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await bookService.DeleteAsync(new BookDeleteRequestDTO { Id = id });
            if (!response.Successful)
            {
                return StatusCode(response.HttpCode, new { response.UserMessage });
            }

            return Ok(new { response.UserMessage });
        }
    }
}
