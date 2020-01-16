using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DemoApps.Models
{
    public class GuestResponse
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Masukkan nama lengkap cuy.")]
        public string NamaLengkap { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Masukkan valid alamat email.")]
        [Required(ErrorMessage = "Masukkan alamat email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Masukkan nomor handphone.")]
        public string Handphone { get; set; }

        [Required(ErrorMessage = "Masukkan status kehadiran saudara.")]
        public bool WillAttend { get; set; }

        [Required(ErrorMessage = "Please choose your event.")]
        [Display(Name = "Event")]

        public int MasterEventID { get; set; }

    }
}