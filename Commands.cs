﻿using System;
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
                    PageNumbers = 420,
                    PublishedOn = new DateTime(1998, 7, 5),
                    Author = richardHall
                },
                new Book
                {
                    Title = "Running with Style",
                    PageNumbers = 69,
                    PublishedOn = new DateTime(2000,1,1),
                    Author = new Author{ Name = "Henry D. Hall", WebUrl = "henryxc.fastrunningblog.com"}
                }
            };
            db.Books.AddRange(books);
            db.SaveChanges(); //I need a function to see if the database exists, and if it does not create it.
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
            Console.WriteLine("Search category: t (title), d (description), p (publishing date), a (author), n (number of pages).");
            Console.Write(" >");
            bookSearchCriteria = Console.ReadLine();
            switch(bookSearchCriteria)
            {
                case "t":
                    SearchBookTitle();
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
            var myList = db.Books.Where(Book => Book.Title.Contains(searchCriteria)).ToList();
            //previous lines returns garbage that can't be iterated on
            foreach (var book in myList)
                    //.Include(book => book.Author)) //fix the print statement, and get the query to return
                    //a list that you can iterate over
            {
                //var webUrl = book.Author.WebUrl ?? "- no web url -";
                Console.WriteLine($"{book.Title} by {book.Author.Name} - {book.PageNumbers} pages.");
                Console.WriteLine("    Published on " + $"{book.PublishedOn:dd-MMM-yyyy}. ");
            }
        }
        public static void SearchAuthors()
        {
            Console.WriteLine("TODO: SearchAuthors()");
        }
    }
}