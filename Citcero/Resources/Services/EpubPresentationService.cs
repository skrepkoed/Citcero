using Citcero.Resources.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersOne.Epub;

namespace Citcero.Resources.Services
{
    public class EpubPresentationService
    {
        public static string GetTextContentFile(EpubLocalTextContentFile textContentFile)
        {
            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(textContentFile.Content);
            StringBuilder sb = new();
            var paragraphs = htmlDocument.DocumentNode.SelectNodes("//p")
                           ?.Select(p => p.InnerText.Trim())
                           .Where(text => !string.IsNullOrWhiteSpace(text))
                           .ToList();
            return paragraphs != null
           ? string.Join("\n", paragraphs.Select(p => $"\t{p}"))
           : string.Empty;
        }

        public static ImageSource? GetBookCover(Book book)
        {
            var epubBook = EpubReader.ReadBook(book.FilePath);
            if (epubBook.CoverImage != null)
            {
                return ImageSource.FromStream(() => new MemoryStream(epubBook.CoverImage));
            }

            return null;

        }
    }
}
