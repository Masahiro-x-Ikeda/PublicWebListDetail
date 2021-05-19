using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebListDetail.Models
{
    public partial class Author
    {

        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }

        [Display(Name = "著者名")]
        public string Name { get; set; }
        public int Age { get; set; }
        public int PrefectureId { get; set; }

        public virtual Prefecture Prefecture { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
