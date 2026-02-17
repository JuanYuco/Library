using Library.Application.DTOs.Author;
using Library.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController(IAuthorService authorService) : Controller
    {
        /// <summary>
        /// It gets all authors created.
        /// </summary>
        /// <returns>It returns an authors list.</returns>
        /// <response code="200">Successful result</response>
        /// <response code="500">Database error</response>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await authorService.GetAllAsync(new Application.DTOs.Author.AuthorCollectionRequestDTO());
            if (!response.Successful)
            {
                return StatusCode(response.HttpCode, new { response.UserMessage });
            }

            return Ok(response.Authors);
        }

        /// <summary>
        /// It gets all authors from database to show all in a drop down list.
        /// </summary>
        /// <returns>It returns an authors list but each object just has id and description</returns>
        /// <response code="200">Successful result</response>
        /// <response code="500">Database error</response>
        [HttpGet("GetAllMinified")]
        public async Task<IActionResult> GetAllMinifiedAsync()
        {
            var response = await authorService.GetAllMinifiedAsync(new Application.DTOs.Author.AuthorMinifiedCollectionRequestDTO());
            if (!response.Successful)
            {
                return StatusCode(response.HttpCode, new { response.UserMessage });
            }

            return Ok(response.Authors);
        }

        /// <summary>
        /// It creates the author on database.
        /// </summary>
        /// <param name="request">Author information to save</param>
        /// <returns>It returns a message with the result information</returns>
        /// <response code="200">Created successfully</response>
        /// <response code="400">There are invalid properties or the author's email is already used</response>
        /// <response code="500">Database error</response>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(AuthorSaveRequestDTO request)
        {
            var response = await authorService.CreateAsync(request);
            if (!response.Successful)
            {
                return StatusCode(response.HttpCode, new { response.UserMessage });
            }

            return Ok(new { response.UserMessage });
        }

        /// <summary>
        /// It deletes the author from database.
        /// </summary>
        /// <param name="id">Author id</param>
        /// <returns>It returns a message with the result information</returns>
        /// <response code="200">Deleted successfully</response>
        /// <response code="400">The id is invalid or the author doesn't exists</response>
        /// <response code="400">The author has books related</response>
        /// <response code="500">Database error</response>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await authorService.DeleteAsync(new AuthorDeleteRequestDTO{ AuthorId = id });
            if (!response.Successful)
            {
                return StatusCode(response.HttpCode, new { response.UserMessage });
            }

            return Ok(new { response.UserMessage });
        }
    }
}
