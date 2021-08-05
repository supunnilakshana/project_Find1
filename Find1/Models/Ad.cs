using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Find1.Models
{
    public class Ad
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [Display(Name = "Title")]
        [StringLength(300)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please choose your service Category")]
        [Display(Name = "Category ")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter Location")]
        [Display(Name = "Location")]
        public string Location { get; set; }


        public string Owner { get; set; }


         [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                  ErrorMessage = "Entered phone format is not valid.")]

        
        public String Mobile { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price Required!")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Please choose profile image")]
        public string ProfilePicture { get; set; }

        [Display(Name = "Created Date")]
        public DateTime Datetime { get; set; }

        [Required(ErrorMessage = "Please enter Description")]
        [Display(Name = "Title")]
        [StringLength(5000)]
        public string Disription { get; set; }


        public Ad()
        {

            Datetime = DateTime.Now;


        }









    }
}
