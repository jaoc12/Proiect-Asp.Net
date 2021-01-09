using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_DAW.Models
{
    public class Award
    {
        public int AwardId { get; set; }

        [MinLength(3, ErrorMessage = "Award name cannot be less than 3!"),
        MaxLength(50, ErrorMessage = "Award name cannot be more than 20!")]
        public string Name { get; set; }

        [MinLength(10, ErrorMessage = "Description cannot be less than 10!"),
         MaxLength(200, ErrorMessage = "Description cannot be more than 200!")]
        public string Description { get; set; }

        // many-to-many cu movie
        public virtual ICollection<Movie> Movies { get; set; }

        [NotMapped]
        public List<Movie> AwardedMovies { get; set; }
    }
}