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
                new BookModel(){ Id = 0, Title ="Genesis", Author = "Moses" },
                new BookModel(){ Id = 1, Title ="Romans", Author = "Paul of Tarsus" },
                new BookModel(){ Id = 2, Title ="Revelation", Author = "John" },
                new BookModel(){ Id = 3, Title ="Proverbs", Author = "King Solomon" },
                new BookModel(){ Id = 4, Title ="The Gospel of Matthew", Author = "Matthew" },
            };
        }
    }
}
