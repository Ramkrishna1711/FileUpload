using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload.Model
{
    public class AccountDetail
    {
        //[RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        //[MaxLength(7, ErrorMessage = "The {0} value should be {1} characters.")]
        //[MinLength(7, ErrorMessage = "The {0} value should be {1} characters.")]
        public string AccountNumber { get; set; }

    }
}
