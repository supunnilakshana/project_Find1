using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Find1.ViewModels
{
    public class FilterViewModel
    {
        [Required(ErrorMessage = "Please choose your service Category")]
        [Display(Name = "Category ")]
        //  public Category Category { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter Location")]
        [Display(Name = "Location")]
        public string Location { get; set; }


        [Display(Name = "Max Price")]
       [Required(ErrorMessage = "Max Price Required!")]
        public float MaxPrice { get; set; }


        [Display(Name = "Min Price")]
        [Required(ErrorMessage = "Min Price Required!")]
        public float MinPrice { get; set; }







    }
}
