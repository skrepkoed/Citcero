using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Citcero.Resources.Models;
using Microsoft.EntityFrameworkCore;
namespace Citcero.Resources.DbServices
{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        
        private readonly string _dbPath;

        public ApplicationDbContext()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dbPath = System.IO.Path.Combine(folder, "citcero.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quote>()
                .HasOne(q => q.Book)
                .WithMany(b => b.Quotes).HasForeignKey(q => q.BookId);

        }
        public List<Quote> GetQuotesByBook(int bookId)
        {
            return [.. Quotes.Where(q => q.BookId == bookId)];

        }
        public List<Quote> GetQuotesOnPage(int pageNumber, int bookId)
        {
            return Quotes.Where(q => q.PageNumber == pageNumber && q.BookId==bookId).ToList();
        }

        public void RemoveAllBookQuotes(int bookId) { 
            Quotes.RemoveRange(GetQuotesByBook(bookId));
        }

    }

}
