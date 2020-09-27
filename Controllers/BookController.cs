using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(BookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
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
            ViewBag.Languages = GetLanguage().Select(x => new SelectListItem()
            {
                Text = x.Text,
                Value = x.Id.ToString()
            }).ToList();
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
            if (ModelState.IsValid) // Check if the book has all the required details
            {
                if (bookModel.CoverPhoto != null) // Check if a cover photo has been uploaded
                {
                    string folder = "books/cover/";
                    bookModel.CoverPhotoUrl = await uploadImage(folder, bookModel.CoverPhoto);
                }

                if (bookModel.Gallery != null) // Check if multiple photos have been uploaded
                {
                    string folder = "books/gallery/";

                    bookModel.GalleryUrl = new List<GalleryModel>();

                    foreach (var file in bookModel.Gallery) {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            Url = await uploadImage(folder, file)
                        };
                        bookModel.GalleryUrl.Add(gallery);
                    }
                }

                if (bookModel.PDF != null) // Check if a PDF file has been uploaded
                {
                    string folder = "books/pdf/";
                    bookModel.PDFUrl = await uploadPDF(folder, bookModel.PDF);
                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0) { return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id }); }
            }

            ViewBag.Languages = new SelectList(GetLanguage(), "Id", "Text");
            ViewBag.IsSuccess = false;
            ViewBag.BookId = 0;
            return View();
        }

        private Task uploadImage(string folder, KeyValuePair<string, StringValues> file)
        {
            throw new NotImplementedException();
        } 

        private async Task<String> uploadImage(string folderPath, IFormFile image)
        {
            folderPath += Guid.NewGuid().ToString() + '_' + image.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await image.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }

        private async Task<String> uploadPDF(string folderPath, IFormFile pdf)
        {
            folderPath += Guid.NewGuid().ToString() + '_' + pdf.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await pdf.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }

        private List<LanguageModel> GetLanguage() {
            return new List<LanguageModel>() { 
                new LanguageModel() { Id = 1, Text = "English"},
                new LanguageModel() { Id = 2, Text = "Chinese"},
                new LanguageModel() { Id = 3, Text = "Japanese"},
            };
        }
    }
}
