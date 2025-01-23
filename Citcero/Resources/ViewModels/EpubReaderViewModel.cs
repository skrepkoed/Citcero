using System.Collections.ObjectModel;
using System.Text;
using Citcero.Resources.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VersOne.Epub;
using HtmlAgilityPack;
using CommunityToolkit.Mvvm.DependencyInjection;
using Citcero.Resources.DbServices;
using PdfSharp.Snippets.Drawing;
using Citcero.Resources.Services;
using Citcero.Resources.Utils;
namespace Citcero.Resources.ViewModels
{
    [QueryProperty(nameof(SelectedBook), "SelectedBook")]
    [QueryProperty(nameof(QuoteSource), "QuoteSource")]
    partial class EpubReaderViewModel : ObservableObject
    {
        [ObservableProperty]
        public Book selectedBook;
        [ObservableProperty]
        public Quote quoteSource;
        [ObservableProperty]
        private int selectedLength;
        [ObservableProperty]
        private int cursor;
        [ObservableProperty]
        private ObservableCollection<EpubPage> pages = new();
        [ObservableProperty]
        private int currentPageIndex = 0;
        [ObservableProperty]
        private bool isEditing;
        [ObservableProperty]
        private FormattedString _currentPageContent;
        [ObservableProperty]
        private List<Quote> quotes;
        [ObservableProperty]
        private int currentPageNumber;

        private ApplicationDbContext _dbContext;

        public EpubReaderViewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        partial void OnSelectedBookChanged(Book book)
        {
            Quotes = _dbContext.GetQuotesByBook(book.Id);
            EpubPage.LoadEpub(book, Pages, _dbContext);
            CurrentPageIndex = 0;
            CurrentPageContent = FormattedTextGenerator.GetFormattedText(Pages[CurrentPageIndex].Content);
            CurrentPageNumber = 1;
            DrawQoute();
        }
        partial void OnQuoteSourceChanged(Quote quote)
        {
            CurrentPageIndex = quote.PageNumber;
            CurrentPageNumber = CurrentPageIndex + 1;
        }




        [RelayCommand]
        private void NextPage()
        {
            if (CurrentPageIndex < Pages.Count - 1)
            {
                CurrentPageIndex++;
                CurrentPageNumber++;
            }
        }

        [RelayCommand]
        private void PreviousPage()
        {
            if (CurrentPageIndex > 0)
            {
                CurrentPageIndex--;
                CurrentPageNumber--;
            }
        }
        partial void OnCurrentPageIndexChanged(int currentIndex)
        {
            CurrentPageContent = FormattedTextGenerator.GetFormattedText(Pages[CurrentPageIndex].Content);
            DrawQoute();
        }
        partial void OnCurrentPageNumberChanged(int oldValue, int newValue)
        {
            if (newValue < Pages.Count + 1)
            {
                CurrentPageIndex = newValue - 1;
            }
        }
        private void DrawQoute()
        {
            if (Pages[CurrentPageIndex].HasQuote)
            {
                CurrentPageContent = HighlightService.UpdatePageTextWithHighlights(CurrentPageContent.ToString(), Pages[CurrentPageIndex].Quotes);
            }
        }

        [RelayCommand]
        private void AddQuote()
        {
            IsEditing = true;
        }
        [RelayCommand]
        private void SaveQuote()
        {
            IsEditing = false;
            if (SelectedLength >= 0)
            {
                Quotes.Add(new Quote
                {
                    Text = CurrentPageContent.ToString().Substring(Cursor, SelectedLength),
                    StartIndex = Cursor,
                    PageNumber = CurrentPageIndex,
                    BookId = selectedBook.Id
                });
            }
            _dbContext.Quotes.Add(Quotes.Last());
            _dbContext.SaveChanges();
            Pages[CurrentPageIndex].HasQuote = true;
            Pages[CurrentPageIndex].Quotes.Add(Quotes.Last());
            DrawQoute();
        }
    }
}