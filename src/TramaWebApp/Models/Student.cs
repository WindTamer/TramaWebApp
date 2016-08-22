using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TramaWebApp.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Display(Name = "Student Name")]
        [Required]
        public string StudentName { get; set; }

        public virtual ICollection<Essay> Essays { get; set; }
    }
}
