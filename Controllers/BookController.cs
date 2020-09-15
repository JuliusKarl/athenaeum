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
        public BookController()
        {
            _bookRepository = new BookRepository();
        }
        public ViewResult GetAllBooks()
        {
            var data =  _bookRepository.GetAllBooks();
            ViewData["Title"] = "All Books";
            return View(data);
        }
        public ViewResult GetBook(int id)
        {
            var data = _bookRepository.GetBookById(id);
            ViewData["Title"] = "Book";
            return View(data);
        }
        public ViewResult SearchBook(string title, string author)
        {
            var data =  _bookRepository.SearchBook(title, author);
            ViewData["Title"] = "Search";
            return View(data);
        }
    }
}
