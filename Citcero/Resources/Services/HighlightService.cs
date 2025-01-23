using Citcero.Resources.Models;
using PdfSharp.Snippets.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citcero.Resources.Services
{
    class HighlightService
    {
        public static FormattedString UpdatePageTextWithHighlights(string PageText, List<Quote> quotes)
        {
            var formattedText = new FormattedString();

            // Сортировать цитаты по позиции
            var sortedQuotes = quotes.OrderBy(q => q.StartIndex).ToList();

            int currentIndex = 0;
            foreach (Quote quote in sortedQuotes)
            {
                // Добавить текст до цитаты
                if (quote.StartIndex > currentIndex)
                {
                    formattedText.Spans.Add(new Span
                    {
                        Text = PageText.Substring(currentIndex, quote.StartIndex - currentIndex)
                    });
                }

                // Добавить цитату с выделением
                formattedText.Spans.Add(new Span
                {
                    Text = PageText.Substring(quote.StartIndex, quote.Text.Length),
                    BackgroundColor = Colors.Yellow
                });

                currentIndex = quote.StartIndex + quote.Text.Length;


                // Добавить текст после цитат
                if (currentIndex < PageText.Length)
                {
                    formattedText.Spans.Add(new Span
                    {
                        Text = PageText.Substring(currentIndex)
                    });
                }
            }
            return formattedText;
        }
    }
}
