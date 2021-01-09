using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proiect_DAW.Models
{
    public class Job
    {
        public int JobId { get; set; }

        [MinLength(2, ErrorMessage = "Type cannot be less than 2!"),
         MaxLength(30, ErrorMessage = "Type cannot be more than 30!")]
        public String Type { get; set; }

        [MinLength(10, ErrorMessage = "Description cannot be less than 10!"),
         MaxLength(200, ErrorMessage = "Description cannot be more than 200!")]
        public String Description { get; set; }
    }
}