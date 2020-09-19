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
                        Id = book.Id
                    });
                }
            }
            return books;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book != null)
            {
                var bookDetails = new BookModel()
                {
                    Author = book.Author,
                    Title = book.Title,
                    Description = book.Description,
                    Id = book.Id
                };
                return bookDetails;
            }
            return null;
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title == title && x.Author == authorName).ToList();
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description
            };

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){ Id = 0, Title ="Genesis", Author = "Moses" , Description = "The first book of the Bible."},
                new BookModel(){ Id = 1, Title ="Romans", Author = "Paul of Tarsus", Description = "The letter of Paul to the Romans."},
                new BookModel(){ Id = 2, Title ="Revelation", Author = "John", Description = "The revelation of John."},
                new BookModel(){ Id = 3, Title ="Proverbs", Author = "King Solomon", Description = "Words of wisdom by King Solomon."},
                new BookModel(){ Id = 4, Title ="Matthew", Author = "Matthew", Description = "The Gospel according to Matthew."},
            };
        }
    }
}
