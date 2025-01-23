using Citcero.Resources.DbServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Citcero.Resources.Models
{
    [Index(nameof(Text), IsUnique = true)]
    public class Quote
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        [NotMapped]
        internal Book Book { get; set; }
        public int PageNumber { get; set; }
        public int StartIndex { get; set; }
        public int BookId { get; set; }

    }
}
