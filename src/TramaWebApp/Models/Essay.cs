using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace TramaWebApp.Models
{
    public class Essay
    {
       [Key]
        public int EssayId { get; set; }
        

        [Display(Name = "Publishing Date")]
        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }

        [Display(Name = "Essay Title")]
        [Required]
        public string EssayTitle { get; set; }

        [Display(Name ="Thesis Statement")]
        [Required]
        public string ThesisStatement { get; set; }

        [Display(Name = "Content")]
        [Required]
        public string Content { get; set; }

        [Display(Name = "Conclusion")]
        [Required]
        public string Conclusion { get; set; }

        

        [Display(Name = "Book Title")]
        public string BookTitle  { get; set; }

        public virtual Book book { get; set; }
       

        [Display(Name = "Essay Author")]
        public string StudentName { get; set; }
        public virtual Student student { get; set; }
        
    }
}
