using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hub.Tables
{
    // Credentials That Customer Enters
    public class OrderDetails
    {
        [Required(ErrorMessage = "Please, Enter Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, Enter Your Surname")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Please, Enter Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, Enter Address")]
        [Display(Name="Adress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please, Enter City")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please, Enter Country")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public bool StudentCheck { get; set; }

      
    }
}
