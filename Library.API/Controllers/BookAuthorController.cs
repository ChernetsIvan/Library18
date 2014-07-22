//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web.Mvc;
//using AutoMapper;
//using Library.API.Utility;
//using Library.API.ViewModels;
//using Library.Model.Models;
//using Library.Service;

//namespace Library.API.Controllers
//{
//    [Authorize]
//    public class BookAuthorController : Controller
//    {
//        private readonly IBookService _bookService;
//        private readonly IAuthorService _authorService;
//        private readonly IBookAuthorService _bookAuthorService;
//        private readonly IBookQrCodeService _bookQrCodeService;
//        private readonly IBookAmountService _bookAmountService;

//        public BookAuthorController(IBookService bookService, IAuthorService authorService, IBookAuthorService bookAuthorService, IBookQrCodeService bookQrCodeService, IBookAmountService bookAmountService)
//        {
//            _bookService = bookService;
//            _authorService = authorService;
//            _bookAuthorService = bookAuthorService;
//            _bookQrCodeService = bookQrCodeService;
//            _bookAmountService = bookAmountService;
//        }

//        public JsonResult BookAuthorList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
//        {
//            try
//            {
//                var bookAuthorVms = GetBookAuthorVms(jtStartIndex, jtPageSize, jtSorting);
//                return Json(new { Result = StrReprs.OK, Records = bookAuthorVms, TotalRecordCount = _bookAuthorService.GetBookAuthorsCount() });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
//            }
//        }    

//        public ActionResult LazyLoading(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
//        {
//            var bookAuthorVms = GetBookAuthorVms(jtStartIndex, jtPageSize, jtSorting, null);
//            JsonForLazyLoading json = new JsonForLazyLoading
//            {
//                NoMoreData = bookAuthorVms.Count() < jtPageSize,
//                HTMLString = RenderPartialViewToString("_BookListPartial", bookAuthorVms)
//            };
//            return Json(json);
//        }

//        public ActionResult Filter(int startIndex = 0, int pageSize = 0, string sorting = null, string filtering = null)
//        {
//            var bookAuthorVms = GetBookAuthorVms(startIndex, pageSize, sorting, filtering);
//            JsonForLazyLoading json = new JsonForLazyLoading
//            {
//                NoMoreData = bookAuthorVms.Count() < pageSize,
//                HTMLString = RenderPartialViewToString("_BookListPartial", bookAuthorVms)
//            };
//            return Json(json);
//        }
                   
//        public JsonResult CreateBookAuthor(BookAuthorViewModel bookAuthorVm)
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
//                Book book = Mapper.Map<BookAuthorViewModel, Book>(bookAuthorVm);
//                string bookId = _bookService.CreateBook(book);
//                _bookQrCodeService.CreateBookQrCode(book);
//                bookAuthorVm.BookId = bookId;
//                BookAuthor bookAuthor = Mapper.Map<BookAuthorViewModel,BookAuthor>(bookAuthorVm);
//                string bookAuthorId = _bookAuthorService.CreateBookAuthor(bookAuthor);
//                bookAuthorVm.BookAuthorId = bookAuthorId;
//                return Json(new { Result = StrReprs.OK, Record = bookAuthorVm });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
//            }
//        }

//        public JsonResult UpdateBookAuthor(BookAuthorViewModel bookAuthorVm)
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
//                Author author = Mapper.Map<BookAuthorViewModel, Author>(bookAuthorVm);
//                _authorService.UpdateAuthor(author);
//                Book book = Mapper.Map<BookAuthorViewModel, Book>(bookAuthorVm);
//                _bookService.UpdateBook(book);
//                _bookQrCodeService.UpdateBookQrCode(book);
//                BookAuthor bookAuthor = Mapper.Map<BookAuthorViewModel, BookAuthor>(bookAuthorVm);
//                _bookAuthorService.UpdateBookAuthor(bookAuthor);
//                return Json(new { Result = StrReprs.OK });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
//            }
//        }

//        public JsonResult DeleteBookAuthor(string bookAuthorId)
//        {
//            try
//            {
//                if (!User.IsInRole(StrReprs.Admin))
//                {
//                    return Json(new { Result = StrReprs.ERROR, Message = StrReprs.DeleteRecordsCanOnlyAdmins });
//                }
//                var bookAuthor = _bookAuthorService.GetBookAuthor(bookAuthorId);
//                string bookId = bookAuthor.BookId;
//                _bookAuthorService.DeleteBookAuthor(bookAuthorId);
//                var qrCode = _bookQrCodeService.GetBookQrCodeByBookId(bookId);
//                _bookQrCodeService.DeleteBookQrCode(qrCode.BookQrCodeId);
//                _bookService.DeleteBook(bookId);
//                return Json(new { Result = StrReprs.OK });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
//            }
//        }

//        private string GetErrorsFromModelState()
//        {
//            return ViewData.ModelState.Values.SelectMany(modelState => modelState.Errors).Aggregate("", (current, error) => current + (" • " + error.ErrorMessage + "  "));
//        }

//        private IEnumerable<BookAuthorViewModel> GetBookAuthorVms(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null, string jtFiltering = null)
//        {
//            IEnumerable<BookAuthor> bookAuthors = new List<BookAuthor>();
//            if (jtFiltering == null) bookAuthors = _bookAuthorService.GetBookAuthors(jtStartIndex, jtPageSize);
//            else bookAuthors = _bookAuthorService.GetBookAuthors(jtStartIndex, jtPageSize, jtFiltering);

//            var bookAuthorVms = from ba in bookAuthors
//                                join a in _authorService.GetAuthors() on ba.AuthorId equals a.AuthorId
//                                join b in _bookService.GetBooks() on ba.BookId equals b.BookId
//                                select new BookAuthorViewModel
//                                {
//                                    BookAuthorId = ba.BookAuthorId,
//                                    BookId = b.BookId,
//                                    //AuthorId = a.AuthorId,
//                                    //AuthorName = a.Name + " " + a.LastName,
//                                    Title = b.Title,
//                                    Isbn = b.Isbn,
//                                    Year = b.Year,
//                                    Description = b.Description,
//                                    PagesAmount = b.PagesAmount,
//                                    PublishingHouse = b.PublishingHouse
//                                };

//            var allAuthors = _authorService.GetAuthors();
//            IEnumerable<AuthorViewModel> listAuthors = new List<AuthorViewModel>();
//            foreach (BookAuthor ba in bookAuthors)
//            {
//                foreach (var allAuthor in allAuthors)
//                {
//                    if(ba.)
//                }
//            }


            
//            foreach (BookAuthor ba in bookAuthors)
//            {
                
//            }

//            BookAuthorViewModel.Sorting(ref bookAuthorVms, jtSorting);
//            return bookAuthorVms;
//        }

//        //private string RenderPartialViewToString(string viewName, object model)
//        //{
//        //    if (string.IsNullOrEmpty(viewName))
//        //        viewName = ControllerContext.RouteData.GetRequiredString("action");

//        //    ViewData.Model = model;

//        //    using (StringWriter sw = new StringWriter())
//        //    {
//        //        ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
//        //        ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
//        //        viewResult.View.Render(viewContext, sw);

//        //        return sw.GetStringBuilder().ToString();
//        //    }
//        //}
//    }
//}
