using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Library.API.Utility;

namespace Library.API.ViewModels
{
    public class BookViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string BookId { get; set; }

        [Required(ErrorMessage = "The book should have at least one author")]
        public List<AuthorViewModel> Authors { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title")]
        [MinLength(1, ErrorMessage = "Minimum length of title is 1")]
        [MaxLength(2000, ErrorMessage = "Maximum length of title is 2000")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [Display(Name = "ISBN")]
        public string Isbn { get; set; }

        [RangeYearToCurrent(1900, ErrorMessage = "Please enter a correct year (from 1900 to now)")]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Additional info")]
        public string Description { get; set; }

        [Display(Name = "Amount of pages")]
        public int PagesAmount { get; set; }

        [Display(Name = "Publishing house")]
        public string PublishingHouse { get; set; }

        [Display(Name = "Amount of books")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum amount of books is 1")]
        [Required(ErrorMessage = "Amount of books is required")]
        public int BookAmount { get; set; }

        public BookViewModel()
        {
            Year = DateTime.Now.Year;
        }
    }
}