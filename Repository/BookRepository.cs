using System;
using BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allBooks = await _context.Books.ToListAsync();
            if (allBooks?.Any() == true)
            {
                foreach (var book in allBooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Title = book.Title,
                        Description = book.Description,
                        Id = book.Id,
                        CoverPhotoUrl = book.CoverPhotoUrl
                    });
                }
            }
            return books;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Title = book.Title,
                    Description = book.Description,
                    Id = book.Id,
                    CoverPhotoUrl = book.CoverPhotoUrl,
                    PDFUrl = book.PDFUrl,
                    GalleryUrl = book.Gallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Url = g.URL
                    }).ToList()
                }).FirstOrDefaultAsync();
        }
        public async Task<List<BookModel>> SearchBook(string title, string authorName)
        {
            var books = new List<BookModel>();
            if (books?.Any() == true)
            {
                foreach (var book in books)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Title = book.Title,
                        Description = book.Description,
                        Id = book.Id,
                        CoverPhotoUrl = book.CoverPhotoUrl
                    });
                }
            }
            return books;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                CoverPhotoUrl = model.CoverPhotoUrl,
                PDFUrl = model.PDFUrl
            };

            newBook.Gallery = new List<Gallery>();

            foreach (var file in model.GalleryUrl)
            {
                newBook.Gallery.Add(new Gallery() { 
                    Name = file.Name,
                    URL = file.Url
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }
    }
}
