using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOffers.Models
{
    public class ValidateFile : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
             var file =  value;

            if(file == null)
            {
                return false;
            }

            return true;
        }
    }
}