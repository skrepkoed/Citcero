using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersOne.Epub;
using Citcero.Resources.Services;
using Citcero.Resources.DbServices;
using System.Collections.ObjectModel;
namespace Citcero.Resources.Models
{
    public class EpubPage
    {
        public int PageNumber { get; set; }
        public string Content { get; set; }
        public bool _hasQuote = false;
        public bool HasQuote { get => _hasQuote; set => _hasQuote = value; }
        public List<Quote> Quotes { get; set; } = new List<Quote>();
        public static void LoadEpub(Book book, ObservableCollection<EpubPage> epubPages, ApplicationDbContext dbContext)
        {
            ICollection<Quote> quotes = dbContext.GetQuotesByBook(book.Id);
            int[] pageNumbersWithQuote = quotes.Select(q => q.PageNumber).Distinct().ToArray();
            EpubBook Epubbook = EpubReader.ReadBook(book.FilePath);

            string content = string.Empty;
            foreach (EpubLocalTextContentFile textContentFile in Epubbook.ReadingOrder)
            {
                content += EpubPresentationService.GetTextContentFile(textContentFile);
            }

            int pageSize = 3000;
            int totalPages = (content.Length + pageSize - 1) / pageSize;

            epubPages.Clear();
            for (int i = 0; i < totalPages; i++)
            {
                var pageText = content.Substring(i * pageSize,
                    Math.Min(pageSize, content.Length - i * pageSize));
                EpubPage epubPage = new EpubPage
                {
                    PageNumber = i,
                    Content = pageText,
                    HasQuote = pageNumbersWithQuote.Contains(i),
                    Quotes = dbContext.GetQuotesOnPage(i,book.Id)
                };

                epubPages.Add(epubPage);
            }
        }
    }
}
