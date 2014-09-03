//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web.Mvc;

//namespace Library.API.ViewModels
//{
//    public class UserBookViewModel : IValidatableObject
//    {
//        [HiddenInput(DisplayValue = false)]
//        public string UserBookId {get; set; }
        
//        [HiddenInput(DisplayValue = false)]
//        public string UserProfileId { get; set; }

//        [HiddenInput(DisplayValue = false)]
//        public string BookId { get; set; }

//        [Required(ErrorMessage = "Please enter user's name")]
//        [Display(Name = "Author's name")]
//        [MinLength(2, ErrorMessage = "Author's minimum length name is 2")]
//        [MaxLength(500, ErrorMessage = "Authors maximum length name is 500")]
//        public string UserName { get; set; }

//        [Required(ErrorMessage = "Please enter user's last name")]
//        [Display(Name = "Author's last name")]
//        [MinLength(2, ErrorMessage = "Author's minimum length last name is 2")]
//        [MaxLength(500, ErrorMessage = "Author's maximum length last name is 500")]
//        public string UserLastName { get; set; }

//        [Required(ErrorMessage = "Please enter a book's title")]
//        [Display(Name = "Title")]
//        [MinLength(1, ErrorMessage = "Minimum length of the book's title is 1")]
//        [MaxLength(2000, ErrorMessage = "Maximum length of the book's title is 2000")]
//        public string BookTitle { get; set; }

//        [Required(ErrorMessage = "Please enter the date of taking book")]
//        [Display(Name = "Date taken")]
//        public DateTime DateTaken { get; set; }

//        [Required(ErrorMessage = "Please enter the return date book")]
//        [Display(Name = "Date returned")]
//        public DateTime DateReturned { get; set; }

//        public UserBookViewModel()
//        {
//            DateTaken = DateTime.Now;
//        }

//        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
//        {
//            if (DateReturned < DateTaken)
//            {
//                return new[] {new ValidationResult("Date returned must be greater than date taken")};
//            }
//            return new[] { ValidationResult.Success };
//        }

//        public static void Sorting(ref IEnumerable<UserBookViewModel> userBookVms, string sorting)
//        {
//            if (string.IsNullOrEmpty(sorting) || sorting.Equals("UserName ASC"))
//            {
//                userBookVms = userBookVms.OrderBy(p => p.UserName);
//            }
//            else if (sorting.Equals("UserName DESC"))
//            {
//                userBookVms = userBookVms.OrderByDescending(p => p.UserName);
//            }
//            else if (sorting.Equals("UserLastName ASC"))
//            {
//                userBookVms = userBookVms.OrderBy(p => p.UserLastName);
//            }
//            else if (sorting.Equals("UserLastName DESC"))
//            {
//                userBookVms = userBookVms.OrderByDescending(p => p.UserLastName);
//            }
//            else if (sorting.Equals("BookTitle ASC"))
//            {
//                userBookVms = userBookVms.OrderBy(p => p.BookTitle);
//            }
//            else if (sorting.Equals("BookTitle DESC"))
//            {
//                userBookVms = userBookVms.OrderByDescending(p => p.BookTitle);
//            }
//            else if (sorting.Equals("DateTaken ASC"))
//            {
//                userBookVms = userBookVms.OrderBy(p => p.DateTaken);
//            }
//            else if (sorting.Equals("DateTaken DESC"))
//            {
//                userBookVms = userBookVms.OrderByDescending(p => p.DateTaken);
//            }
//            else if (sorting.Equals("DateReturned ASC"))
//            {
//                userBookVms = userBookVms.OrderBy(p => p.DateReturned);
//            }
//            else if (sorting.Equals("DateReturned DESC"))
//            {
//                userBookVms = userBookVms.OrderByDescending(p => p.DateReturned);
//            }
//        }
//    }
//}