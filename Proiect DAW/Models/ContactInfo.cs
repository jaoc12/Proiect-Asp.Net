using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proiect_DAW.Models
{
    public class ContactInfo
    {
        public int ContactInfoId { get; set; }

        [RegularExpression(@"^\w+@\w+.\w{2,}$", ErrorMessage = "This is not a valid email address!")]
        public String ContactEmail { get; set; }

        [RegularExpression(@"^07(\d{8})$", ErrorMessage = "This is not a valid phone number!")]
        public String ContactPhone { get; set; }

        // one-to-one cu Person
        public virtual Person Person { get; set; }
    }
}