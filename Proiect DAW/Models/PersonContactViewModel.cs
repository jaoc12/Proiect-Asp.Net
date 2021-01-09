using Proiect_DAW.Models.MyValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_DAW.Models
{
    public class PersonContactViewModel
    {
        [RegularExpression(@"^[a-zA-z_]+$", ErrorMessage = "A valid name only contains letters!"),
         MinLength(2, ErrorMessage = "Name cannot be less than 2!"),
         MaxLength(30, ErrorMessage = "Name cannot be more than 30!")]
        public String Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true),
         BirthdateValidator2]
        public DateTime Birthday { get; set; }

        [RegularExpression(@"^\w+@\w+.\w{2,}$", ErrorMessage = "This is not a valid email address!")]
        public String ContactEmail { get; set; }

        [RegularExpression(@"^07(\d{8})$", ErrorMessage = "This is not a valid phone number!")]
        public String ContactPhone { get; set; }

        public int PersonId { get; set; }
    }
}