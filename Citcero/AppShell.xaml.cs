namespace Citcero
{
    using Citcero.Resources.Views;
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BooksView), typeof(BooksView));
            Routing.RegisterRoute(nameof(EpubReaderView), typeof(EpubReaderView));
            Routing.RegisterRoute(nameof(QuotesView), typeof(QuotesView));

        }

    }
}
