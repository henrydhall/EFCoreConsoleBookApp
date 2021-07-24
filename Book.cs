using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreConsoleBookApp
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public DateTime PublishedOn { get; set; }
        public int AuthorId { get; set; }
        public int PageNumbers { get; set; }
        //-------------------------------------
        //Relations
        public Author Author { get; set; }
    }
}
