using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proiect_DAW.Models
{
    public class Credit
    {
        public int CreditId { get; set; }

        // one-to-many cu Job
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public Job Job { get; set; }

        // one-to-many cu Person
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }

        // one-to-many cu Movie
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}