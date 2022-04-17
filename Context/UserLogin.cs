using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirstApp.Context
{
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public int password { get; set; }
        [Required]
        public int Mobile { get; set; }

    }
}
