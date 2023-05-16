using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
    public class Lecture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Lecture Name")]
        public string LectureName { get; set; }

        [Required]
        [MaxLength(5000)]
        [DisplayName("Context")]
        public string LectureContext { get; set; }


        [DisplayName("Date of Creation")]
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;

        [DisplayName("Image")]
        public string LectureImageUrl { get; set; }




    }
}
