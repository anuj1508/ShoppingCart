using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.ViewModel
{
    public class LoginCustomer
    {
        [EmailAddress]
        [Required]
        public string Email_id { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
