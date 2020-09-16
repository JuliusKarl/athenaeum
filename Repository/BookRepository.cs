using System;
using BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }
        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title == title && x.Author == authorName).ToList();
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
