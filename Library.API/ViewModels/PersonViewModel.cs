using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Library.Web.ViewModels
{
    public class PersonViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string UserProfileId { get; set; }
        
        [Required(ErrorMessage = "Please enter the name")]
        [Display(Name = "User name")]
        [MinLength(2, ErrorMessage = "Minimum length is 2")]
        [MaxLength(500, ErrorMessage = "Maximum length is 500")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the lastname")]
        [Display(Name = "User last name")]
        [MinLength(2, ErrorMessage = "Minimum length is 2")]
        [MaxLength(500, ErrorMessage = "Maximum length is 500")]
        public string LastName { get; set; }       
    }
}