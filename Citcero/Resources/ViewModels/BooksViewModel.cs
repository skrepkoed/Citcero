using Citcero.Resources.DbServices;
using Citcero.Resources.Models;
using Citcero.Resources.Services;
using Citcero.Resources.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Citcero.Resources.ViewModels
{
    partial class BooksViewModel : ObservableObject
    {
        private readonly ApplicationDbContext _dbContext;
        // Коллекция книг
        [ObservableProperty]
        public static ObservableCollection<Book> books = new();
        [ObservableProperty]
        public Book selectedBook;

        public BooksViewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            LoadBooks();
        }

        private void LoadBooks()
        {
            Books = new ObservableCollection<Book>(_dbContext.Books.ToList());
        }

        public void ResetSelectedBook()
        {
            SelectedBook = null;
        }

        partial void OnBooksChanged(ObservableCollection<Book> books)
        {
            Book.LoadBookCovers(Books);
        }

        // Метод для команды добавления книги
        [RelayCommand]
        public void AddBook()
        {
            Books.Add(new Book
            {
                Title = string.Empty,
                Author = string.Empty,
                Isbn = string.Empty,
                IsEditing = true
            });
        }
        [RelayCommand]
        public void SaveBook(Book book)
        {
            if (book.Id == 0)
            {
                _dbContext.Books.Add(book);
            }
            else
            {
                _dbContext.Books.Update(book);
            }

            _dbContext.SaveChanges();
            book.IsEditing = false;
            LoadBooks();
        }
        [RelayCommand]
        public void EditBook(Book book)
        {
            if (book == null) return;

            book.IsEditing = true; 
        }


        [RelayCommand]
        public void DeleteBook(Book book)
        {
            if (book.Id != 0)
            {
                _dbContext.RemoveAllBookQuotes(book.Id);
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }
            
            Books.Remove(book);
        }

        [RelayCommand]
        private async Task AddBookFromEpubAsync()
        {
            try
            {
                // Открываем диалоговое окно для выбора файла
                var result = await EpubLoaderService.LoadEpubFromFile();

                if (result == null) return; // Пользователь отменил выбор

                var filePath = result.FullPath;

                // Извлекаем метаданные PDF
                var (title, author) = EpubLoaderService.GetEpubMetadata(filePath);

                // Добавляем книгу в коллекцию
                Books.Add(new Book
                {
                    Title = title,
                    Author = author,
                    Isbn = string.Empty,
                    IsEditing = false,
                    FilePath = filePath
                });
                SaveBook(Books.Last());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to add book from EPUB. Please try again.", "OK");
            }
        }

        [RelayCommand]
        private async Task OpenEpubReaderView()
        {
            await Shell.Current.GoToAsync(nameof(EpubReaderView), true, new Dictionary<string, object>
                {
                    { "SelectedBook", SelectedBook }
                }); ;
            SelectedBook = null;

        }
        [RelayCommand]
        private void OpenQuotesView(Book book)
        {
            Shell.Current.GoToAsync(nameof(QuotesView), true, new Dictionary<string, object>
                {
                    { "BookQuotes", book }
                }); ;
        }
    }
}
