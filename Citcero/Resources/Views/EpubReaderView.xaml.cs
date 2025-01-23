using Citcero.Resources.DbServices;
using Citcero.Resources.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Citcero.Resources.Views;

public partial class EpubReaderView : ContentPage
{
    public EpubReaderView()
    {
		InitializeComponent();
		var dbContext = new ApplicationDbContext();
        BindingContext = new EpubReaderViewModel(dbContext);
    }
    

    private void OnButtonClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EpubReaderViewModel;
        //var selectedText = editor.Text.Substring(editor.CursorPosition, editor.SelectionLength);
        viewModel.Cursor = editor.CursorPosition;
        viewModel.SelectedLength = editor.SelectionLength;
        viewModel.SaveQuoteCommand.Execute(null);
    }
}