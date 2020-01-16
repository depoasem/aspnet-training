using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DemoApps.Models
{
    public class loginResponse
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Masukkan username.")]
        public string Username { get; set; }
        [DataType(DataType.Password)]

        [Required(ErrorMessage = "Masukkan password.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}