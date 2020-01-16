using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DemoApps.Models
{
    public class UserMasterResponse
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Masukkan username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Masukkan password.")]
        public string Password { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Masukkan valid alamat email.")]
        [Required(ErrorMessage = "Masukkan alamat email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Masukkan status kehadiran saudara.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please choose your event.")]
        [Display(Name = "Role")]

        public string Role { get; set; }
    }
}