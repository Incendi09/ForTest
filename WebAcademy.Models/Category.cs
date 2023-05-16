using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;

namespace Academy.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }

        [DisplayName("Date of Creation")]
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;

        [DisplayName("Image")]
        [ValidateNever]
        public string CategoryImageUrl { get; set; }


    }
}
