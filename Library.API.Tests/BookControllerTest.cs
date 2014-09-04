//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using System.Web;
//using Library.API.Controllers;
//using Library.API.Utility;
//using Library.API.ViewModels;
//using Library.Data.Infrastructure;
//using Library.Data.Repository;
//using Library.Model.Models;
//using Library.Service;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Library.API;
//using Moq;

//namespace Library.API.Tests
//{
//    [TestClass]
//    public class BookControllerTest
//    {
//        private Mock<IBookRepository> _bookRepository;
//        private Mock<IAuthorRepository> _authorRepository;
//        private Mock<IBookAuthorRepository> _bookAuthorRepository;
//        private Mock<IBookAmountRepository> _bookAmountRepository;
//        private Mock<IBookQrCodeRepository> _bookQrCodeRepository;

//        private IBookService _bookService;
//        private IAuthorService _authorService;
//        private IBookAuthorService _bookAuthorService;
//        private IBookAmountService _bookAmountService;
//        private IBookQrCodeService _bookQrCodeService;

//        private Mock<IUnitOfWork> _unitOfWork;

//        private Mock<IIdentity> _identity;
//        private Mock<IPrincipal> _principal;

//        [TestInitialize]
//        public void ConstuctBeforeRunningEachTest()
//        {
//            _bookRepository = new Mock<IBookRepository>();
//            _authorRepository = new Mock<IAuthorRepository>();
//            _bookAuthorRepository = new Mock<IBookAuthorRepository>();
//            _bookAmountRepository = new Mock<IBookAmountRepository>();
//            _bookQrCodeRepository = new Mock<IBookQrCodeRepository>();

//            _unitOfWork = new Mock<IUnitOfWork>();

//            _bookService = new BookService(_bookRepository.Object, _unitOfWork.Object);
//            _authorService = new AuthorService(_authorRepository.Object, _unitOfWork.Object);
//            _bookAuthorService = new BookAuthorService(_bookAuthorRepository.Object, _unitOfWork.Object, _authorRepository.Object, _bookRepository.Object);
//            _bookAmountService = new BookAmountService(_bookAmountRepository.Object, _unitOfWork.Object);
//            _bookQrCodeService = new BookQrCodeService(_bookQrCodeRepository.Object, _unitOfWork.Object);

//            _identity = new Mock<IIdentity>();
//            _principal = new Mock<IPrincipal>();
//        }

//        [TestMethod]
//        public void Get()
//        {
//            // Arrange
//            IEnumerable<Book> books = new List<Book>
//            {
//                new Book() {BookId = "BookId1", Title = "Title1", Description = "Descr1", Isbn = "Isbn1", PagesAmount = 100, PublishingHouse = "PublHouse1", Year = 2001},
//                new Book() {BookId = "BookId2", Title = "Title2", Description = "Descr2", Isbn = "Isbn2", PagesAmount = 200, PublishingHouse = "PublHouse2", Year = 2002},
//                new Book() {BookId = "BookId3", Title = "Title3", Description = "Descr3", Isbn = "Isbn3", PagesAmount = 300, PublishingHouse = "PublHouse3", Year = 2003}
//            }.AsEnumerable();
//            _bookRepository.Setup(x => x.GetAll()).Returns(books);

//            IEnumerable<Author> authors = new List<Author>
//            {
//                new Author() {AuthorId = "AuthorId1", Name = "AuthorName1", LastName = "AuthorLastName1"},
//                new Author() {AuthorId = "AuthorId2", Name = "AuthorName2", LastName = "AuthorLastName2"},
//                new Author() {AuthorId = "AuthorId3", Name = "AuthorName3", LastName = "AuthorLastName3"}
//            }.AsEnumerable();
//            _authorRepository.Setup(x => x.GetAll()).Returns(authors);

//            IEnumerable<BookAuthor> bookAuthors = new List<BookAuthor>
//            {
//                new BookAuthor() {BookAuthorId = "BookAuthorId1", AuthorId = "AuthorId1", BookId = "BookId1"},
//                new BookAuthor() {BookAuthorId = "BookAuthorId2", AuthorId = "AuthorId1", BookId = "BookId2"},
//                new BookAuthor() {BookAuthorId = "BookAuthorId3", AuthorId = "AuthorId1", BookId = "BookId3"},
//                new BookAuthor() {BookAuthorId = "BookAuthorId4", AuthorId = "AuthorId2", BookId = "BookId3"},
//                new BookAuthor() {BookAuthorId = "BookAuthorId5", AuthorId = "AuthorId3", BookId = "BookId2"}
//            }.AsEnumerable();
//            _bookAuthorRepository.Setup(x => x.GetAll()).Returns(bookAuthors);

//            IEnumerable<BookAmount> bookAmounts = new List<BookAmount>
//            {
//                new BookAmount() {BookAmountId = "BookAmountId1", BookId = "BookId1", Amount = 3},
//                new BookAmount() {BookAmountId = "BookAmountId2", BookId = "BookId2", Amount = 4},
//                new BookAmount() {BookAmountId = "BookAmountId3", BookId = "BookId3", Amount = 1}
//            }.AsEnumerable();
//            _bookAmountRepository.Setup(x => x.GetAll()).Returns(bookAmounts);


//            BookController controller = new BookController(_bookService, _authorService, _bookAuthorService, _bookQrCodeService, _bookAmountService);

//            // Act
//            //IEnumerable<FullBookViewModel> result = controller.GetFullBookVms(0, 10, null) as List<FullBookViewModel>;

//            //// Assert
//            //Assert.IsNotNull(result);
//            //Assert.AreEqual(result.Count(), 3);
//            //Assert.AreEqual(result.FirstOrDefault(x => x.BookId == "BookId1").Authors.Count, 2);
//            //Assert.AreEqual(result.FirstOrDefault(x => x.BookId == "BookId2").Authors.Count, 2);
//            //Assert.AreEqual(result.FirstOrDefault(x => x.BookId == "BookId3").Authors.Count, 2);
//            //Assert.AreEqual(result.FirstOrDefault(x => x.BookId == "BookId1").Authors.FirstOrDefault(a => a.AuthorId == "AuthorId1").AuthorId, "AuthorId1");
//            //Assert.AreEqual(result.FirstOrDefault(x => x.BookId == "BookId2").Authors.FirstOrDefault(a => a.AuthorId == "AuthorId1").AuthorId, "AuthorId1");
//            //Assert.AreEqual(result.FirstOrDefault(x => x.BookId == "BookId3").Authors.FirstOrDefault(a => a.AuthorId == "AuthorId1").AuthorId, "AuthorId1");


//            //result.Data.Records[]
//            //Assert.AreEqual(2, result.Count()); +		System.Web.Http.Results.JsonResult<<>f__AnonymousType2<string,System.Collections.Generic.IEnumerable<Library.API.ViewModels.FullBookViewModel>,int>>]	{System.Web.Http.Results.JsonResult<<>f__AnonymousType2<string,System.Collections.Generic.IEnumerable<Library.API.ViewModels.FullBookViewModel>,int>>}	System.Web.Http.Results.JsonResult<<>f__AnonymousType2<string,System.Collections.Generic.IEnumerable<Library.API.ViewModels.FullBookViewModel>,int>>

//            //Assert.AreEqual("value1", result.ElementAt(0));
//            //Assert.AreEqual("value2", result.ElementAt(1));
//        }
//    }
//}
