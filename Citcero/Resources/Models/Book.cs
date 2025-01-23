using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using Citcero.Resources.Services;

namespace Citcero.Resources.Models
{
    public partial class Book : ObservableObject
    {
        [Key]
        public int Id { get; set; }
        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string author;

        [ObservableProperty]
        private string isbn;

        [ObservableProperty]
        private string filePath;

        private bool _isEditing;

        [NotMapped]  
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }
        public ICollection<Quote> Quotes { get; set; } = new List<Quote>();
        [NotMapped]
        public ImageSource? CoverImage { get; set; }
        public Book()
        {
            _isEditing = false;
        }

        public static void LoadBookCovers(ObservableCollection<Book> books)
        {
            foreach (var book in books)
            {
                // Извлечение обложки книги
                book.CoverImage = EpubPresentationService.GetBookCover(book);
            }
        }
    }
}
