using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citcero.Resources.Services
{
    public static  class PdfMetadataReader
    {
        public static (string Title, string Author) GetPdfMetadata(string filePath)
        {
            
                using var pdfDocument = PdfReader.Open(filePath, PdfDocumentOpenMode.Import);

                var title = pdfDocument.Info.Title;
                var author = pdfDocument.Info.Author;
                
                return (title ?? "Unknown Title", author ?? "Unknown Author");
            
                return ("Unknown Title", "Unknown Author");
            
        }
    }
}
