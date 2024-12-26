using Citcero.Resources.Models;
using Citcero.Resources.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        // Коллекция книг
        [ObservableProperty]
        public static ObservableCollection<Book> books = new();

        // Команда для добавления книги
        public ICommand AddBookCommand { get; }
        public ICommand SaveBookCommand { get; }
        public ICommand EditBookCommand { get; }
        public ICommand AddBookFromPdfCommand { get; }

        // Конструктор
        public BooksViewModel()
        {
            // Заполняем коллекцию книг для примера
            if (Books.Count == 0)
            {
                Books.Add(new Book { Title = "1984", Author = "George Orwell", Isbn = "1234567890" });
                Books.Add(new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Isbn = "0987654321" });
            }
            // Инициализируем команду
            AddBookCommand = new RelayCommand(AddBook);
            SaveBookCommand = new RelayCommand<Book>(SaveBook);
            EditBookCommand = new RelayCommand<Book>(EditBook);
            AddBookFromPdfCommand = new AsyncRelayCommand(AddBookFromPdfAsync);
        }

        // Метод для команды добавления книги
        private void AddBook()
        {
            Books.Add(new Book
            {
                Title = string.Empty,
                Author = string.Empty,
                Isbn = string.Empty,
                IsEditing = true // Включаем режим редактирования
            });
        }
        private void SaveBook(Book book)
        {
            // Сохраняем книгу и выключаем режим редактирования
            if (book == null)
            {
                Console.WriteLine("Book is null!");
                return;
            }
            book.IsEditing = false;
        }
        private void EditBook(Book book)
        {
            if (book == null) return;

            book.IsEditing = true; // Включаем режим редактирования
        }

        private async Task AddBookFromPdfAsync()
        {
            try
            {
                // Открываем диалоговое окно для выбора файла
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a PDF file",
                    FileTypes = FilePickerFileType.Pdf
                });

                if (result == null) return; // Пользователь отменил выбор

                var filePath = result.FullPath;

                // Извлекаем метаданные PDF
                var (title, author) = PdfMetadataReader.GetPdfMetadata(filePath);

                // Добавляем книгу в коллекцию
                Books.Add(new Book
                {
                    Title = title,
                    Author = author,
                    Isbn = string.Empty,
                    IsEditing = false
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding book from PDF: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error", "Failed to add book from PDF. Please try again.", "OK");
            }
        }
    }
}
