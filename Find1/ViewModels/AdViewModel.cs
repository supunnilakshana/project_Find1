using Microsoft.AspNetCore.Http;
using Find1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Find1.Models
{
    public class AdViewModel
    {
        

        [Required(ErrorMessage = "Please enter first name")]
        [Display(Name = "Title")]
        [StringLength(300)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please choose your service Category")]
        [Display(Name = "Category ")]
      //  public Category Category { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter Location")]
        [Display(Name = "Location")]
        public string Location { get; set; }


     

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                  ErrorMessage = "Entered phone format is not valid.")]
        public String Mobile { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price Required!")]
        public float Price { get; set; }


        [Required(ErrorMessage = "Please enter Description")]
        [Display(Name = " Description")]
        [StringLength(5000)]
        public string Disription { get; set; }



        [Required(ErrorMessage = "Please choose  image")]
        [Display(Name = "Advertiesment Picture")]
        public IFormFile AdImage { get; set; }






    }
}
