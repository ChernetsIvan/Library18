using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Library.API.Utility;
using Library.Core.Models;
using Library.Service;
using Library.API.ViewModels;

namespace Library.API.Controllers
{
    [Authorize]
    public class AuthorController : ApiController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }       

        public IHttpActionResult Get(int startIndex, int pageSize, string sorting, string filtering)
        {
            try
            {
                IEnumerable<AuthorModel> authors = new List<AuthorModel>();
                if (filtering == "-1")
                    authors = _authorService.GetAuthors(startIndex, pageSize, sorting);
                else
                    authors = _authorService.GetAuthors(pageSize, filtering);

                List<AuthorViewModel> authorVms = authors.Select(Mapper.Map<Author, AuthorViewModel>).ToList();
                return Json(new { Result = StrReprs.OK, Records = authorVms, TotalRecordCount = _authorService.GetAuthorsCount() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = StrReprs.ERROR, Message = ex.Message });
            }
        }

        public IHttpActionResult Post([FromBody]AuthorViewModel authorVm)
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
                Author author = Mapper.Map<AuthorViewModel, Author>(authorVm);
                string id = _authorService.CreateAuthor(author);
                authorVm.AuthorId = id;
                return Json(new { Result = StrReprs.OK, Record = authorVm });
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
