using Library.Web.Models.DTOs.Book;
using Library.Web.Models.ViewModels.Book;
using Library.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Controllers
{
    public class BookController(IBookService bookService, IAuthorService authorService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var indexViewModel = new IndexViewModel();
            var response = await bookService.GetAllAsync();
            if (response.Successful)
            {
                indexViewModel.Books = response.Books;
            }
            else
            {
                TempData["errorMessage"] = response.UserMessage;
            }

            return View(indexViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var authorResponse = await authorService.GetAllMinifiedAsync();
            if (authorResponse.Successful)
            {
                ViewBag.Authors = new SelectList(authorResponse.Authors, "Id", "Description"); ;
            } else
            {
                TempData["errorMessage"] = authorResponse.UserMessage;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await bookService.DeleteAsync(id);
            if (response.Successful)
            {
                TempData["userMessage"] = response.UserMessage;
            }
            else
            {
                TempData["errorMessage"] = response.UserMessage;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDTO book)
        {
            if (!ModelState.IsValid) return View(book);

            var response = await bookService.CreateAsync(book);
            if (response.Successful)
            {
                TempData["userMessage"] = response.UserMessage;
            }
            else
            {
                TempData["errorMessage"] = response.UserMessage;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
