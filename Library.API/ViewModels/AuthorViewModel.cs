using System.ComponentModel.DataAnnotations;

namespace Library.API.ViewModels
{
    public class AuthorViewModel
    {
        //[HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }

        [Required(ErrorMessage = "Please enter author's name")]
        [Display(Name = "Author's name")]
        [MinLength(2, ErrorMessage = "Minimum length of author's name is 2")]
        [MaxLength(1000, ErrorMessage = "Maximum length of author's name is 1000")]
        [RegularExpression(@"^\w{1,500}\s\w{1,500}$", ErrorMessage = "Please enter author's name and last name")]
        public string Name { get; set; }
    }
}