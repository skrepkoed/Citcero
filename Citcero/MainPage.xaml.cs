
using Citcero.Resources.Views;

namespace Citcero
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }



        async void OpenBooksPage(object sender, EventArgs e)
        {
            // Переход по маршруту
            await Shell.Current.GoToAsync(nameof(BooksView));
        }
    }

}
