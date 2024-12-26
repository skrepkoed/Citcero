using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citcero.Resources.Models
{
    partial class Book : ObservableObject
    {
        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string author;

        [ObservableProperty]
        private string isbn;

        [ObservableProperty]
        private bool isEditing;

        //public string Publisher { get; set; }
        //public string Language { get; set; }
        //public string Description { get; set; }
        //public string PageCount { get; set; }
        //public string Thumbnail { get; set; }
        //public string SmallThumbnail { get; set; }
    }
}
