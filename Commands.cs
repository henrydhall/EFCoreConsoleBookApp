using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCoreConsoleBookApp
{
    public static class Commands
    {
        public static void ListAll()
        {
            using(var db = new ConsoleBooksDbContext())
            {
                foreach(var book in db.Books.AsNoTracking()
                    .Include(book => book.Author))
                {
                    var webUrl = book.Author.WebUrl ?? "- no web url -";
                    Console.WriteLine($"{book.Title} by {book.Author.Name} - {book.PageNumbers} pages.");
                    Console.WriteLine("    Published on " + $"{book.PublishedOn:dd-MMM-yyyy}. {webUrl}");
                }
            }
        }
        public static bool Initialize(bool onlyIfNoDatabase)
        {
            using (var db = new ConsoleBooksDbContext())
            {
                if (onlyIfNoDatabase && (db.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    return false;

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                if (!db.Books.Any())
                {
                    WriteTestData(db);
                    Console.WriteLine("Seeded database");
                }
            }
            return true;
        }
        public static void WriteTestData(this ConsoleBooksDbContext db)
        {
            var richardHall = new Author
            {
                Name = "Richard Hall",
                WebUrl = "saxophone.com"
            };
            var books = new List<Book>
            {
                new Book
                {
                    Title = "Saxophones",
                    PageNumbers = 320,
                    PublishedOn = new DateTime(1998, 7, 5),
                    Author = richardHall
                },
                new Book
                {
                    Title = "Running with Style",
                    PageNumbers = 70,
                    PublishedOn = new DateTime(2000,1,1),
                    Author = new Author{ Name = "Henry D. Hall", WebUrl = "henryxc.fastrunningblog.com"}
                }
            };
            db.Books.AddRange(books);
            db.SaveChanges(); 
        }
        public static void SearchDb()
        {
            string tableToSearch;
            Console.WriteLine("TODO: searchdb()");
            Console.WriteLine("Search category: a (author), b (book)");
            Console.Write(" >");
            tableToSearch = Console.ReadLine();
            switch (tableToSearch)
            {
                case "b":
                    SearchBooks();
                    break;
                case "a":
                    SearchAuthors();
                    break;
                default:
                    Console.WriteLine("Not a category.");
                    break;
            }
        }
        public static void SearchBooks()
        {
            string bookSearchCriteria;
            Console.WriteLine("Search category: t (title), p (publishing date), a (author), n (number of pages).");
            Console.Write(" >");
            bookSearchCriteria = Console.ReadLine();
            switch(bookSearchCriteria)
            {
                case "t":
                    SearchBookTitle();
                    break;
                case "n":
                    SearchBookPageNumber();
                    break;
                default:
                    Console.WriteLine("Not a category");
                    break;
            }
        }
        public static void SearchBookTitle()
        {
            string searchCriteria;
            Console.Write("Search criteria: ");
            searchCriteria = Console.ReadLine();
            var db = new ConsoleBooksDbContext();
            foreach(var book in db.Books.AsNoTracking()
                    .Where( book => book.Title.Contains(searchCriteria))
                    .Include(book => book.Author) )
                {
                    var webUrl = book.Author.WebUrl ?? "- no web url -";
                    Console.WriteLine($"{book.Title} by {book.Author.Name} - {book.PageNumbers} pages.");
                    Console.WriteLine("    Published on " + $"{book.PublishedOn:dd-MMM-yyyy}. {webUrl}");
                }
        }
        public static void SearchBookPageNumber()
        {
            string searchCriteria;
            int searchBase;
            Console.WriteLine("Enter a number. The query will search for books within 50 pages of the number.");
            Console.Write("Search criteria: ");
            searchCriteria = Console.ReadLine();
            try
            {
                searchBase = Convert.ToInt32(searchCriteria);
            }
            catch(System.FormatException)
            {
                Console.WriteLine("Invalid input.");
                return;
            }
            var db = new ConsoleBooksDbContext();
            foreach(var book in db.Books.AsNoTracking()
                .Where( book => book.PageNumbers > searchBase - 50 && book.PageNumbers < searchBase + 50 )
                .Include(book => book.Author) )
            {
                var webUrl = book.Author.WebUrl ?? "- no web url -";
                Console.WriteLine($"{book.Title} by {book.Author.Name} - {book.PageNumbers} pages.");
                Console.WriteLine("    Published on " + $"{book.PublishedOn:dd-MMM-yyyy}. {webUrl}");
            }
        }
        public static void ListAuthors()
        {
            HashSet<Author> Authors = new HashSet<Author>();
            using (var db = new ConsoleBooksDbContext())
            {
                foreach (var book in db.Books.AsNoTracking()
                    .Include(book => book.Author))
                {
                    Authors.Add(book.Author);
                }
            }
            foreach (Author author in Authors)
            {
                Console.WriteLine($"{author.Name} - {author.WebUrl}");
            }
        }
        public static void MakeChange()
        {
            Console.WriteLine("Type of change: e (existing book), a (add new book), d (delete book).");
            Console.Write(" >");
            string changeType;
            changeType = Console.ReadLine();
            switch( changeType )
            {
                case "e":
                    EditBook();
                    break;
                case "a":
                    AddBook();
                    break;
                case "d":
                    DeleteBook();
                    break;
                default:
                    Console.WriteLine("Invalid type of change.");
                    break;
            }
        }
        public static void EditBook()
        {
            Console.WriteLine("TODO: EditBook()");
        }
        public static void AddBook()
        {
            //variables needed to add a book and author to the database
            string bookTitle;
            int publishedYear;
            int publishedMonth;
            int publishedDay;
            DateTime bookPublished;
            int bookPageNumbers;
            string authorName;
            string authorUrl;
            //database reference
            var db = new ConsoleBooksDbContext();
            //collecting input from user and assigning variables
            Console.Write("Book title: ");
            bookTitle = Console.ReadLine();
            Console.Write("Year published: ");
            publishedYear = Convert.ToInt32(Console.ReadLine());
            Console.Write("Month published: ");
            publishedMonth = Convert.ToInt32(Console.ReadLine());
            Console.Write("Day published: ");
            publishedDay = Convert.ToInt32(Console.ReadLine());
            bookPublished = new DateTime(publishedYear, publishedMonth, publishedDay);
            Console.Write("Number of pages: ");
            bookPageNumbers = Convert.ToInt32(Console.ReadLine());
            Console.Write("Author name: ");
            authorName = Console.ReadLine();
            Console.Write("Author web url: ");
            authorUrl = Console.ReadLine();
            //creating book
            var books = new List<Book>
            {
                new Book
                {
                    Title = bookTitle,
                    PageNumbers = bookPageNumbers,
                    PublishedOn = bookPublished,
                    Author = new Author{ Name = authorName, WebUrl = authorUrl }
                }
            };
            //adding book to database
            db.Books.AddRange(books);
            db.SaveChanges();
        }
        public static void DeleteBook()
        {
            Console.WriteLine("TODO: DeleteBook()");
        }
        public static void SearchAuthors()
        {
            Console.WriteLine("TODO: SearchAuthors()");
        }
    }
}
