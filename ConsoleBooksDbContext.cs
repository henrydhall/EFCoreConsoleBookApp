using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConsoleBookApp
{
    public class ConsoleBooksDbContext : DbContext
    {
        private const string ConnectionString =
            @"Server=(localdb)\mssqllocaldb;
             Database=ConsoleBooksDatabase;
             Trusted_Connection=True";
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}

//TODO: I'd very much like to see what happens when I try to update the database schema