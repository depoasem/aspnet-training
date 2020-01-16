using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DemoApps.Models
{
    public class EventsMasterResponse
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Please enter the event name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please complete the event description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the event date.")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Please enter the event expired date.")]
        public DateTime EventExpiredDate { get; set; }

    }
}