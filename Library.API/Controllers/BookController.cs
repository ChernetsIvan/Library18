using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using Library.API.Utility;
using Library.API.ViewModels;
using Library.Service;

namespace Library.API.Controllers
{
    [System.Web.Http.Authorize]
    public class BookController : ApiController
    {

        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public BookController(IBookService bookService, IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        public IHttpActionResult Get(int startIndex = 0, int pageSize = 0, string sorting = null)
        {
            try
            {

                return Json(new { Result = StrReprs.OK, Records = fullBookVms, TotalRecordCount = fullBookVms.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
            }
        }

        public IHttpActionResult Post([FromBody]BookViewModel bookVm) //частично реализован!!!!!!!!!!!!!
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
                Book book = Mapper.Map<BookViewModel, Book>(bookVm);
                string id = _bookService.CreateBook(book);
                bookVm.BookId = id;
                return Json(new { Result = StrReprs.OK, Record = bookVm });
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
    }
}
