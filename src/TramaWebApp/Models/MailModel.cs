using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TramaWebApp.Models
{
    public class MailModel
    {
        
        [EmailAddress] 
        [Required]
        [Display(Name = "Your Email")]       
        public string From { get; set; }

        [Required]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Phone]
        [Required]
        [Display(Name = "Your Phone")]
        public string Phone { get; set; }

        
        [MinLength(5)]
        [MaxLength(5000)]
        [Required]
        [Display(Name = "Message Body")]
        public string Body { get; set; }
    }
}
