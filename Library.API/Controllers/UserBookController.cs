//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using AutoMapper;
//using Library.API.Utility;
//using Library.Model.Models;
//using Library.Service;
//using Library.Web.ViewModels;

//namespace Library.API.Controllers
//{
//    public class UserBookController : Controller // "Посмотреть книги на руках"
//    {
//        private readonly IBookService _bookService;
//        private readonly IUserProfileService _userProfileService;
//        private readonly IUserBookService _userBookService;

//        public UserBookController(IBookService bookService, IUserProfileService userProfileService,
//            IUserBookService userBookService)
//        {
//            _bookService = bookService;
//            _userProfileService = userProfileService;
//            _userBookService = userBookService;
//        }

//        //public ViewResult UserBooks()
//        //{
//        //    return View();
//        //}

//        [HttpPost]
//        public JsonResult UserBookList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
//        {
//            try
//            {
//                IEnumerable<UserBookViewModel> userBookVms = from ub in _userBookService.GetUserBooks(jtStartIndex, jtPageSize)
//                    join up in _userProfileService.GetUserProfiles() on ub.UserProfileId equals up.UserProfileId
//                    join b in _bookService.GetBooks() on ub.BookId equals b.BookId
//                    select new UserBookViewModel
//                    {
//                        UserBookId = ub.UserBookId,
//                        BookId = b.BookId,
//                        UserProfileId = up.UserProfileId,
//                        BookTitle = b.Title,
//                        DateReturned = ub.DateReturned,
//                        DateTaken = ub.DateTaken,
//                        UserName = up.Name,
//                        UserLastName = up.LastName,
//                    };
//                UserBookViewModel.Sorting(ref userBookVms, jtSorting);
//                return Json(new {Result = StrReprs.OK, Records = userBookVms, TotalRecordCount = _userBookService.GetUserBookCount() });
//            }
//            catch (Exception ex)
//            {
//                return Json(new {Result = StrReprs.ERROR, Message = ex.Message});
//            }
//        }
        
//        [HttpPost]
//        public JsonResult UserList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
//        {
//            try
//            {
//                IEnumerable<UserProfile> userProfiles = _userProfileService.GetUserProfiles( jtStartIndex, jtPageSize, jtSorting);
//                List<PersonViewModel> personVms = userProfiles.Select(Mapper.Map<UserProfile, PersonViewModel>).ToList();
//                return Json(new { Result = StrReprs.OK, Records = personVms, TotalRecordCount = _userProfileService.GetUserProfileCount() });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
//            }
//        }

//        //[HttpPost]
//        //// Возвращает только не взятые другими пользователями книжки
//        //public JsonResult BookList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null) 
//        //{
//        //    try
//        //    {
//        //        IEnumerable<Book> enumerable = _bookService.GetBooks(jtStartIndex, jtPageSize, jtSorting);
//        //        var allBooks = enumerable as IList<Book> ?? enumerable.ToList();
//        //        var givenBooks = from allBook in allBooks
//        //            join userBook in _userBookService.GetUserBooks() on allBook.BookId equals userBook.BookId
//        //            select new Book
//        //            {
//        //                BookId = allBook.BookId,
//        //                Isbn = allBook.Isbn,
//        //                Title = allBook.Title,
//        //                Year = allBook.Year
//        //            };
//        //        var givenBooks2 = givenBooks as IList<Book> ?? givenBooks.ToList();

//        //        IEnumerable<Book> stayBooks = allBooks.Except(givenBooks2, new BookComparer());
//        //        _bookService.Sorting(ref stayBooks, jtSorting);
//        //        List<BookViewModel> bookVms = stayBooks.Select(Mapper.Map<Book, BookViewModel>).ToList();
//        //        return Json(new { Result = StrReprs.OK, Records = bookVms, TotalRecordCount = _bookService.GetBookCount() });
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
//        //    }
//        //}


//        [HttpPost]
//        public JsonResult CreateUserBook(UserBookViewModel userBookVm)
//        {
//            try
//            {
//                if (!User.IsInRole(StrReprs.Admin))
//                {
//                    return Json(new { Result = StrReprs.ERROR, Message = StrReprs.AddNewRecordsCanOnlyAdmins });
//                }
//                if (!ModelState.IsValid)
//                {
//                    return Json(new { Result = StrReprs.ERROR, Message = GetErrorsFromModelState() });
//                }               
//                UserBook userBook = Mapper.Map<UserBookViewModel, UserBook>(userBookVm);
//                string userBookId = _userBookService.CreateUserBook(userBook);
//                userBookVm.UserBookId = userBookId;
//                return Json(new { Result = StrReprs.OK, Record = userBookVm });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
//            }
//        }

//        [HttpPost]
//        public JsonResult UpdateUserBook(UserBookViewModel userBookVm)
//        {
//            try
//            {
//                if (!User.IsInRole(StrReprs.Admin))
//                {
//                    return Json(new { Result = StrReprs.ERROR, Message = StrReprs.UpdateRecordsCanOnlyAdmins });
//                }
//                if (!ModelState.IsValid)
//                {
//                    return Json(new { Result = StrReprs.ERROR, Message = GetErrorsFromModelState() });
//                }                
//                UserBook userBook = Mapper.Map<UserBookViewModel, UserBook>(userBookVm);
//                _userBookService.UpdateUserBook(userBook);
//                return Json(new { Result = StrReprs.OK, Record = userBookVm });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
//            }
//        }

//        [HttpPost]
//        public JsonResult DeleteUserBook(string userBookId)
//        {
//            try
//            {
//                if (!User.IsInRole(StrReprs.Admin))
//                {
//                    return Json(new { Result = StrReprs.ERROR, Message = StrReprs.DeleteRecordsCanOnlyAdmins });
//                }
//                var userBook = _userBookService.GetUserBook(userBookId);
//                _userBookService.DeleteUserBook(userBook.UserBookId);
//                return Json(new { Result = StrReprs.OK });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
//            }
//        }

//        private string GetErrorsFromModelState()
//        {
//            return ViewData.ModelState.Values.SelectMany(modelState => modelState.Errors)
//                .Aggregate("", (current, error) => current + (" • " + error.ErrorMessage + "  "));
//        }

//    }
//}
