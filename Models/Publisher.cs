using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebListDetail.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }

        [Display(Name = "出版社名")]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
