using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Citcero.Resources.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Citcero.Resources.DbServices;
using CommunityToolkit.Mvvm.Input;
using Citcero.Resources.Views;
namespace Citcero.Resources.ViewModels
{
    [QueryProperty(nameof(BookQuotes), "BookQuotes")]
    public partial class QuotesViewModel : ObservableObject
    {
        private readonly ApplicationDbContext _dbContext;
        [ObservableProperty]
        public static ObservableCollection<Quote> quotes = new ObservableCollection<Quote>();

        [ObservableProperty]
        public Book bookQuotes;

        public QuotesViewModel()
        {
            _dbContext = new ApplicationDbContext();
            
            
        }

        partial void OnBookQuotesChanged(Book book)
        {
            LoadQuotes();
        }


        public void LoadQuotes()
        {
            var quotesFromDb = _dbContext.Quotes
                .Where(q => q.BookId == bookQuotes.Id)
                .ToList();

            Quotes.Clear();
            foreach (var quote in quotesFromDb)
            {
                Quotes.Add(quote);
            }
        }
        [RelayCommand]
        private void DeleteQuote(Quote quote)
        {
            if (quote.Id != 0)
            {
                _dbContext.Quotes.Remove(quote);
                _dbContext.SaveChanges();
            }
            Quotes.Remove(quote);

        }
        [RelayCommand]
        private void OpenEpubReader(Quote quote )
        {
           Shell.Current.GoToAsync(nameof(EpubReaderView), true, new Dictionary<string, object>
           {
                { "SelectedBook", BookQuotes },
                {"QuoteSource", quote }
           }); ;
        }
    }
}
