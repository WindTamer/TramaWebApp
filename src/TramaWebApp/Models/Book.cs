using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TramaWebApp.Models
{
    public class Book
    {

        [Key]
        public int BookId { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Author")]
        [Required]
        public string Author { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }


        public virtual ICollection<Essay> Essays { get; set; }


    }
}
