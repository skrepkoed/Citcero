using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersOne.Epub;

namespace Citcero.Resources.Services
{
    public static class EpubLoaderService
    {
        public static (string Title, string Author) GetEpubMetadata(string filePath)
        {

            {
                EpubBook book = EpubReader.ReadBook(filePath);
                string author = book.Author.ToString();
                string title = book.Title.ToString();
                return (title ?? "Unknown Title", author ?? "Unknown Author");
            }

        }

        public static async Task<FileResult> LoadEpubFromFile()
        {
            return await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select a EPUB file",

            });

        }

    }
}
