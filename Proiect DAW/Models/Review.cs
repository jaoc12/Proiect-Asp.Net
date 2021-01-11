using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_DAW.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        
        [MinLength(10, ErrorMessage = "Description cannot be less than 10!"),
         MaxLength(200, ErrorMessage = "Description cannot be more than 200!")]
        public string Content { get; set; }

        [Range(1, 10, ErrorMessage = "Rating should be between 1 and 10")]
        public decimal Rating { get; set; }

        [MinLength(2, ErrorMessage = "Author name cannot be less than 2!"),
         MaxLength(50, ErrorMessage = "Author name cannot be more than 50!")]
        public string AuthorEmail { get; set; }

        // one-to-many cu Movie
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}