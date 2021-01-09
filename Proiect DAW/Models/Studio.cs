using Proiect_DAW.Models.MyValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_DAW.Models
{
    public class Studio
    {
        public int StudioId { get; set; }

        [MinLength(5, ErrorMessage = "Studio name cannot be less than 5!"),
         MaxLength(50, ErrorMessage = "Studio name cannot be more than 50!")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true),
         FoundingValidator]
        public DateTime FoundingDate { get; set; }

        [RegularExpression(@"^[a-zA-z\s]+$", ErrorMessage = "A valid CEO name only contains letters!"),
         MinLength(2, ErrorMessage = "CEO Name cannot be less than 2!"),
         MaxLength(30, ErrorMessage = "CEO Name cannot be more than 30!")]
        public string CEO { get; set; }

        // many-to-one cu Movie
        public virtual ICollection<Movie> Movies { get; set; }

        [NotMapped]
        public List<CheckBoxViewModel> MoviesList { get; set; }
    }
}