using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Language { get; set; }
        [Required(ErrorMessage ="Cover Photo is required!")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverPhotoUrl { get; set; }
        public List<IFormFile> Gallery { get; set; }
        public List<GalleryModel> GalleryUrl { get; set; }
    }
}
