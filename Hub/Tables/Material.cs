using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hub
{
    // Material Table 
   public class Material
    {
        [HiddenInput(DisplayValue=false)]
        [Display(Name= "ID")]
        public int MaterialID { get; set; }

        [Display(Name= "Title")]
        [Required(ErrorMessage ="Enter The Title")]
        public string Name { get; set; }


        [Display(Name = "Creator")]
        [Required(ErrorMessage ="Enter The Creator Name")]
        public string Author { get; set; }
      
        [DataType(DataType.MultilineText)]
        [Display(Name="Description")]
        [Required(ErrorMessage = "Enter The Description")]
        public string Portrayal { get; set; }
        
        [Display(Name="Subject")]
        [Required(ErrorMessage = "Enter The Subject")]
        public string Subject { get; set; }

        [Display(Name="Cost UAH")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Here Must Be Positive Cost Entered")]
        public decimal Cost { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }


      



    }
}
