using Citcero;
using Citcero.Resources.DbServices;
using Citcero.Resources.ViewModels;
namespace Citcero.Resources.Views {
	
	public partial class BooksView : ContentPage
	{
		public BooksView()
		{
			InitializeComponent();
            var dbContext = new ApplicationDbContext();
            BindingContext = new BooksViewModel(dbContext);
        }


	}
}