using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citcero.Resources.Models
{
    internal class RelatedTerms
    {
        public string Term { get; set; }
        public string Definition { get; set; }
        public Quote Quote { get; set; }
        public string Date { get; set; }
    }
}
