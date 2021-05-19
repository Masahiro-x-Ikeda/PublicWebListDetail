using System;
using System.Collections.Generic;

#nullable disable

namespace WebListDetail.Models
{
    public partial class Prefecture
    {
        public Prefecture()
        {
            Authors = new HashSet<Author>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
