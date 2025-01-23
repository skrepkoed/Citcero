using Citcero;
using Citcero.Resources.ViewModels;

namespace Citcero.Resources.Views
{

	public partial class QuotesView : ContentPage
	{
		public QuotesView()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is QuotesViewModel viewModel)
            {
                viewModel.LoadQuotes();
            }
        }
    }
}