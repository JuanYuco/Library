using Library.Web.Models.DTOs.Author;
using Library.Web.Models.ViewModels.Author;
using Library.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class AuthorController(IAuthorService authorService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var indexViewModel = new IndexViewModel();
            var response = await authorService.GetAllAsync();
            if (response.Successful)
            {
                indexViewModel.Authors = response.Authors;
            } else
            {
                TempData["errorMessage"] = response.UserMessage;
            }
                
            return View(indexViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorDTO author)
        {
            if (!ModelState.IsValid) return View(author);

            var response = await authorService.CreateAsync(author);
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
        public async Task<IActionResult> Delete(int id)
        {
            var response = await authorService.DeleteAsync(id);
            if (response.Successful)
            {
                TempData["userMessage"] = response.UserMessage;
            } else
            {
                TempData["errorMessage"] = response.UserMessage;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
