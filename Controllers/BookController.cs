using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [Route("All")]
        public async Task<ViewResult> GetAllBooks()
        {
            var data =  await _bookRepository.GetAllBooks();
            ViewData["Title"] = "All Books";
            return View(data);
        }
        [Route("Book")]
        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            ViewData["Title"] = "Book";
            return View(data);
        }
        [Route("Search")]
        public ViewResult SearchBook(string title, string author)
        {
            var data =  _bookRepository.SearchBook(title, author);
            ViewData["Title"] = "Search";
            return View(data);
        }
        [Route("New")]
        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.Languages = new List<String>() { "English", "Korean", "Japanese", "Chinese" };
            ViewData["Title"] = "New Book";
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }
        [HttpPost]
        [Route("New")]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            ViewData["Title"] = "New Book";
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0) { return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id }); }
            }
            ViewBag.IsSuccess = false;
            ViewBag.BookId = 0;
            return View();
        }
    }
}
