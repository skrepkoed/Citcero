using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Citcero.Resources.Models
{
    internal class Quote
    {
        public string Text { get; set; }
        //public string Author { get; set; }
        public  Book Book { get; set; }
        //public string Tags { get; set; }
        //public string Category { get; set; }
        public string Date { get; set; }
        
    }
}
