//using System;
//using System.Web.Http;
//using Library.Model.Models;
//using Library.Service;

//namespace Library.API.Controllers
//{
//    [Authorize]
//    public class BookQrCodeController : ApiController
//    {
//        private readonly IBookQrCodeService _bookQrCodeService;
//        public BookQrCodeController(IBookQrCodeService bookQrCodeService)
//        {
//            _bookQrCodeService = bookQrCodeService;
//        }       

//        public IHttpActionResult Get(string bookId)
//        {
//            BookQrCode bookQrCode = _bookQrCodeService.GetBookQrCodeByBookId(bookId);
//            if (bookQrCode != null)
//            {
//                byte[] img = bookQrCode.QrImageData;
//                var data = Convert.ToBase64String(img);
//                return Json(data);
//            }
//            return null;
//        }
//    }
//}
