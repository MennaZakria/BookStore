using BookStoreDAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBL.ModelView
{
   public class BookMW
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Fileupload { get; set; }

        public int AuthorId { get; set; }
   
        public Author Author { get; set; }
    }
}
