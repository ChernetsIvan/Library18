//using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

//namespace Library.API.ViewModels
//{
//    public class UserProfileViewModel
//    {
//        [HiddenInput(DisplayValue = false)]
//        public string UserProfileId { get; set; }

//        [Required]
//        [Display(Name = "Role")]
//        public bool Rights { get; set; } //true - если admin
//        public bool AllowRights { get; set; }

//        [Required]
//        [Display(Name = "Login")]
//        public string Login { get; set; }

//        [Required]
//        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
//        [DataType(DataType.Password)]
//        [Display(Name = "Password")]
//        public string Password { get; set; }

//        [DataType(DataType.Password)]
//        [Display(Name = "Confirm password")]
//        [System.ComponentModel.DataAnnotations.CompareAttribute("Password", ErrorMessage = "The password and confirmation password do not match")]
//        public string ConfirmPassword { get; set; }

//        [Required(ErrorMessage = "Please enter the name")]
//        [Display(Name = "Name")]
//        [MinLength(2, ErrorMessage = "Minimum length is 2")]
//        [MaxLength(500, ErrorMessage = "Maximum length is 500")]
//        public string Name { get; set; }

//        [Required(ErrorMessage = "Please enter the lastname")]
//        [Display(Name = "Last name")]
//        [MinLength(2, ErrorMessage = "Minimum length is 2")]
//        [MaxLength(500, ErrorMessage = "Maximum length is 500")]
//        public string LastName { get; set; }

//        [Required]
//        [Display(Name = "Gender")]
//        public GenderState Gender { get; set; }

//        [Required(ErrorMessage = "Please enter your skype-login")]
//        [Display(Name = "Skype")]
//        [MinLength(2, ErrorMessage = "Minimum length is 2")]
//        [MaxLength(1000, ErrorMessage = "Maximum length is 1000")]
//        public string Skype { get; set; }

//        [Display(Name = "E-Mail")]
//        [EmailAddress(ErrorMessage = "Invalid Email")]
//        [MaxLength(1000, ErrorMessage = "Maximum length is 1000")]
//        public string Mail { get; set; }

//        [Display(Name = "Phone")]
//        [RegularExpression(@"^\(\+\d{1,3}\)-\(\d{1,3}\)-\d{5,12}$", ErrorMessage = "Invalid Phone Number! Format: (+X)-(X)-XXXXX")]
//        [MaxLength(22, ErrorMessage = "Maximum length is 22")]
//        public string Phone { get; set; }

//        [Required(ErrorMessage = "Please enter the floor's number")]
//        [Range(1, 200, ErrorMessage = "Please enter a correct floor")]
//        [Display(Name = "Floor")]
//        public int Floor { get; set; }

//        [Required(ErrorMessage = "Please enter the room's number")]
//        [Display(Name = "Room")]
//        [Range(1, 99999, ErrorMessage = "Please enter a correct room")]
//        public int Room { get; set; }

//        [Display(Name = "Place description")]
//        public string PlaceDescription { get; set; }

//        public UserProfileViewModel()
//        {
//            Phone = "(+375)-()-";
//        }
//    }

//    public enum GenderState
//    {
//        Male,
//        Female
//    }
//}