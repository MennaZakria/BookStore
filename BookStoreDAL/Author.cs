using System;
using System.ComponentModel.DataAnnotations;

namespace BookStoreDAL
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}
