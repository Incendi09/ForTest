using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseName { get; set; }

        [Required]
        [StringLength(250)]
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string CourseImageUrl { get; set; }

        [DisplayName("Date of Creation")]
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;

        [Required]
        [Range(1,7)]
        public int Semester { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
    }
}
