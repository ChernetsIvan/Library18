using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using Library.API.Utility;
using Library.API.ViewModels;
using Library.Model.Models;
using Library.Service;

namespace Library.API.Controllers
{
    [System.Web.Http.Authorize]
    public class BookController : ApiController
    {

        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IBookAuthorService _bookAuthorService;
        private readonly IBookQrCodeService _bookQrCodeService;
        private readonly IBookAmountService _bookAmountService;

        public BookController(IBookService bookService, IAuthorService authorService, IBookAuthorService bookAuthorService, IBookQrCodeService bookQrCodeService, IBookAmountService bookAmountService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _bookAuthorService = bookAuthorService;
            _bookQrCodeService = bookQrCodeService;
            _bookAmountService = bookAmountService;
        }

        public IHttpActionResult Get(int startIndex = 0, int pageSize = 0, string sorting = null)
        {
            try
            {
                IEnumerable<Book> books = _bookService.GetBooks(startIndex, pageSize, sorting);
                var bookAuthors = from b in books  //count of bookAuthors may be not equals count of books!
                                  join ba in _bookAuthorService.GetBookAuthors() on b.BookId equals ba.BookId
                                  select new BookAuthor
                                  {
                                      BookAuthorId = ba.BookAuthorId,
                                      BookId = ba.BookId,
                                      AuthorId = ba.AuthorId
                                  };
                var bookAmounts = from b in books
                                  join ba in _bookAmountService.GetBookAmounts() on b.BookId equals ba.BookId
                                  select new BookAmount
                                  {
                                      BookAmountId = ba.BookAmountId,
                                      BookId = ba.BookId,
                                      Amount = ba.Amount
                                  };
                var fullBookVms = from b in books
                                  join ba in bookAmounts on b.BookId equals ba.BookId
                                  select new FullBookViewModel
                                  {
                                      BookId = b.BookId,
                                      Title = b.Title,
                                      Isbn = b.Isbn,
                                      Year = b.Year,
                                      Description = b.Description,
                                      PagesAmount = b.PagesAmount,
                                      PublishingHouse = b.PublishingHouse,
                                      BookAmount = ba.Amount,
                                      Authors = new List<AuthorViewModel>()
                                  };
                //List<FullBookViewModel> fullBookVms2 = fullBookVms.Distinct().ToList();
                foreach (var fullBookVm in fullBookVms)
                {
                    fullBookVm.Authors.AddRange(_bookAuthorService.GetAuthorsByBookId(fullBookVm.BookId).Select(Mapper.Map<Author, AuthorViewModel>).ToList());
                    //foreach (var ba in bookAuthors)
                    //{
                    //    if (ba.BookId == fullBookVm.BookId)
                    //    {
                    //        fullBookVm.Authors.Add(Mapper.Map<Author, AuthorViewModel>(_bookAuthorService.GetAuthorByBookId(ba.BookId)));
                    //    }
                    //}
                }
                return Json(new { Result = StrReprs.OK, Records = fullBookVms, TotalRecordCount = fullBookVms.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
            }
        }

        public IHttpActionResult Post([FromBody]FullBookViewModel fullBookVm) //частично реализован!!!!!!!!!!!!!
        {
            try
            {
                if (!User.IsInRole(StrReprs.Admin))
                {
                    return Json(new { Result = StrReprs.ERROR, Message = StrReprs.AddNewRecordsCanOnlyAdmins });
                } 
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = StrReprs.ERROR, Message = GetErrorsFromModelState() });
                }                              
                Book book = Mapper.Map<FullBookViewModel, Book>(fullBookVm);
                string id = _bookService.CreateBook(book);
                fullBookVm.BookId = id;
                return Json(new { Result = StrReprs.OK, Record = fullBookVm });
            }
            catch (Exception ex)
            {
                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
            }
        }

        public IHttpActionResult Put(AuthorViewModel authorVm)
        {
            try
            { 
                if (!User.IsInRole(StrReprs.Admin))
                {
                    return Json(new { Result = StrReprs.ERROR, Message = StrReprs.UpdateRecordsCanOnlyAdmins });
                }
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = StrReprs.ERROR, Message = GetErrorsFromModelState() });
                }               
                Author author = Mapper.Map<AuthorViewModel, Author>(authorVm);
                _authorService.UpdateAuthor(author);
                return Json(new { Result = StrReprs.OK });
            }
            catch (Exception ex)
            {
                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
            }
        }

        public IHttpActionResult Delete(string id) // !! - ONLY so!, see for details http://social.msdn.microsoft.com/Forums/en-US/8a383373-326b-47e1-afd6-21bb05a59db8/webapi-v2-visual-studio-2013-iis-express-http-405-delete-method-not-allowed?forum=wcf
        {
            try
            {
                if (!User.IsInRole(StrReprs.Admin))
                {
                    return Json(new { Result = StrReprs.ERROR, Message = StrReprs.AddNewRecordsCanOnlyAdmins });
                }
                _authorService.DeleteAuthor(id);
                return Json(new { Result = StrReprs.OK });
            }
            catch (Exception ex)
            {
                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
            }
        }

        private string GetErrorsFromModelState()
        {
            return ModelState.Values.SelectMany(modelState => modelState.Errors)
                .Aggregate("", (current, error) => current + (" • " + error.ErrorMessage + "  "));
        }

        //private IEnumerable<BookAuthorViewModel> GetBookAuthorVms(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null, string jtFiltering = null)
        //{
        //    IEnumerable<BookAuthor> bookAuthors = new List<BookAuthor>();
        //    if (jtFiltering == null) bookAuthors = _bookAuthorService.GetBookAuthors(jtStartIndex, jtPageSize);
        //    else bookAuthors = _bookAuthorService.GetBookAuthors(jtStartIndex, jtPageSize, jtFiltering);

        //    var bookAuthorVms = from ba in bookAuthors
        //                        join a in _authorService.GetAuthors() on ba.AuthorId equals a.AuthorId
        //                        join b in _bookService.GetBooks() on ba.BookId equals b.BookId
        //                        select new BookAuthorViewModel
        //                        {
        //                            BookAuthorId = ba.BookAuthorId,
        //                            BookId = b.BookId,
        //                            //AuthorId = a.AuthorId,
        //                            //AuthorName = a.Name + " " + a.LastName,
        //                            Title = b.Title,
        //                            Isbn = b.Isbn,
        //                            Year = b.Year,
        //                            Description = b.Description,
        //                            PagesAmount = b.PagesAmount,
        //                            PublishingHouse = b.PublishingHouse
        //                        };

        //    var allAuthors = _authorService.GetAuthors();
        //    IEnumerable<AuthorViewModel> listAuthors = new List<AuthorViewModel>();
        //    foreach (BookAuthor ba in bookAuthors)
        //    {
        //        foreach (var allAuthor in allAuthors)
        //        {
        //            if(ba.)
        //        }
        //    }


            
        //    foreach (BookAuthor ba in bookAuthors)
        //    {
                
        //    }

        //    BookAuthorViewModel.Sorting(ref bookAuthorVms, jtSorting);
        //    return bookAuthorVms;
        //}

        //private string RenderPartialViewToString(string viewName, object model)
        //{
        //    if (string.IsNullOrEmpty(viewName))
        //        viewName = ControllerContext.RouteData.GetRequiredString("action");

        //    ViewData.Model = model;

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
        //        ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
        //        viewResult.View.Render(viewContext, sw);

        //        return sw.GetStringBuilder().ToString();
        //    }
        //}
    }
}
