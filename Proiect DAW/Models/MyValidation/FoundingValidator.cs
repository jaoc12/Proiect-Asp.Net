using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proiect_DAW.Models.MyValidation
{
    public class FoundingValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Studio studio = (Studio)validationContext.ObjectInstance;
            var foundingDate = studio.FoundingDate;

            var cond = false;

            if(foundingDate.Year >= 1900 && foundingDate.Year <= DateTime.Today.Year)
            {
                cond = true;
            }

            return cond ? ValidationResult.Success : new ValidationResult("The founding year is invalid!");
        }
    }
}