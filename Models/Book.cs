using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebListDetail.Models
{
    public partial class Book
    {
        public int Id { get; set; }

        [Display(Name = "書籍名")]
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }

        [Display(Name = "価格")]
        public int Price { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Isbn { get; set; }

        public virtual Author Author { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
