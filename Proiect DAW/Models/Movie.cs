using Proiect_DAW.Models.MyValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_DAW.Models
{
    public class Movie
    {
        public int MovieId {get; set;}

        [MinLength(2, ErrorMessage = "Movie title cannot be less than 2!"),
         MaxLength(50, ErrorMessage = "Movie title cannot be more than 50!")]
        public String Title { get; set; }

        [MinLength(10, ErrorMessage = "Type cannot be less than 10!"),
         MaxLength(300, ErrorMessage = "Type cannot be more than 300!")]
        public String Description { get; set; }

        // many-to-one cu Credit
        public virtual ICollection<Credit> Credits { get; set; }

        // one-to-many cu Studio
        [ForeignKey("Studio")]
        public int StudioId { get; set; }
        public Studio Studio { get; set; }

        // many-to-many cu award
        public virtual ICollection<Award> Awards { get; set; }

        // many-to-one cu Review
        public virtual ICollection<Review> Reviews { get; set; }

        [NotMapped]
        public List<CheckBoxViewModel> AwardsList { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> StudiosList { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> PeopleList { get; set; }

        [NotMapped]
        public List<Job> JobsList { get; set; }

        [NotMapped]
        public List<CheckBoxViewModel> CreditsList { get; set; }

        public string AverageRating()
        {
            if (Reviews.Count() > 0)
            {
                var rating = Reviews.Average(r => r.Rating).ToString();
                return rating;
            }
            else
            {
                return "Not rated";
            }
        }
    }
}