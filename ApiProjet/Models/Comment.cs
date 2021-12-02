using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiProjet.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field is required")]
        [MaxLength(50, ErrorMessage = "Max 20 char")]
        [MinLength(3, ErrorMessage = "Min 3 char")]
        public string Author { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field is required")]
        [MaxLength(50, ErrorMessage = "Max 50 char")]
        [MinLength(3, ErrorMessage = "Min 5 char")]
        public string Body { get; set; }
        public string Email { get; set; }
        public int? PostId { get; set; }
        public string UrlPhoto { get; set; }
    }
}
