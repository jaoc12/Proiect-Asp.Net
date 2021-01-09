using Proiect_DAW.Models.MyValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_DAW.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        [RegularExpression(@"^[a-zA-z\s]+$", ErrorMessage = "A valid name only contains letters!"),
         MinLength(2, ErrorMessage = "Name cannot be less than 2!"),
         MaxLength(30, ErrorMessage = "Name cannot be more than 30!")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true),
         BirthdateValidator1]
        public DateTime Birthday { get; set; }

        // one-to-one cu ContactInfo
        [Required]
        public virtual ContactInfo ContactInfo { get; set; }

        [NotMapped]
        public List<Movie> MoviesList { get; set; }
    }
}