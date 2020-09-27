using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverPhotoUrl { get; set; }
        public ICollection<Gallery> Gallery { get; set; }
        public string PDFUrl { get; set; }
    }
}
