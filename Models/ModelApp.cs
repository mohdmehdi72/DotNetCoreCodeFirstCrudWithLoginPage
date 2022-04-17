using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirstApp.Models
{
    public class ModelApp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int password { get; set; }
        [Compare("password")]
        public int ConfirmPassword { get; set; }
        [Required]
        public int Mobile { get; set; }

    }
}
