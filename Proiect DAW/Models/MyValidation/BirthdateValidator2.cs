using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proiect_DAW.Models.MyValidation
{
    public class BirthdateValidator2 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PersonContactViewModel person = (PersonContactViewModel)validationContext.ObjectInstance;
            var birthdate = person.Birthday;

            var cond = false;
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;

            if(age >= 12 && age <= 150)
            {
                cond = true;
            }

            return cond ? ValidationResult.Success : new ValidationResult("The age of the person is invalid!");
        }
    }
}