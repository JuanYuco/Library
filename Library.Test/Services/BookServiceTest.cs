using Library.Application.Services;
using Library.Application.Settings;
using Library.Domain.Enitities;
using Library.Domain.Ports;
using Microsoft.Extensions.Options;
using Moq;

namespace Library.Test.Services
{
    public class BookServiceTest
    {
        [Fact]
        public async Task GetAllAsync_ItMustReturnAllElements()
        {
            var mockData = new List<Book>
            {
                new Book { Id = 1, Title = "Una breve historia del tiempo", Gender="Ciencia", PagesNumber=194, AuthorId = 1, Year = 1988, Author = new Author {  Id = 1, FullName = " Stephen Hawking" } },
                new Book { Id = 2, Title = "El diario de Ana Frank", Gender="Biografía", PagesNumber=400, AuthorId = 2, Year = 1947, Author = new Author {  Id = 2, FullName = "Otto Frank" } }
            };

            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var bookRepositoryMock = new Mock<IBookRepository>();
            var optionsMock = new Mock<IOptions<LibrarySetting>>();

            bookRepositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(mockData);

            var bookService = new BookService(bookRepositoryMock.Object, authorRepositoryMock.Object, optionsMock.Object);

            var result = await bookService.GetAllAsync(new Application.DTOs.Book.BookCollectionRequestDTO());
            Assert.True(result.Successful, "An error ocurrred consulting the information.");

            var books = result.Books;
            Assert.Equal(mockData.Count, books.Count);
            foreach (var mockItem in mockData)
            {
                var book = books.Where(x => x.Id == mockItem.Id).FirstOrDefault();

                Assert.NotNull(book);
                Assert.Equal(mockItem.Title, book.Title);
                Assert.Equal(mockItem.Gender, book.Gender);
                Assert.Equal(mockItem.PagesNumber, book.PagesNumber);
                Assert.Equal(mockItem.Author.FullName, book.Author);
                Assert.Equal(mockItem.Year, book.Year);
            }

            bookRepositoryMock.Verify(rep => rep.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_GoodEscenary_ItMustReturnASuccessfulTrue()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var bookRepositoryMock = new Mock<IBookRepository>();
            var optionsMock = new Mock<IOptions<LibrarySetting>>();

            var muckBook = new Book { Id = 1, Title = "Una breve historia del tiempo", Gender = "Ciencia", PagesNumber = 194, AuthorId = 1, Year = 1988, Author = new Author { Id = 1, FullName = " Stephen Hawking" } };

            bookRepositoryMock
                .Setup(rep => rep.GetByIdAsync(muckBook.Id))
                .ReturnsAsync(muckBook);

            bookRepositoryMock
                .Setup(rep => rep.DeleteAsync(muckBook));

            var bookService = new BookService(bookRepositoryMock.Object, authorRepositoryMock.Object, optionsMock.Object);
            var result = await bookService.DeleteAsync(new Application.DTOs.Book.BookDeleteRequestDTO() { Id = muckBook.Id });
            Assert.True(result.Successful, "An error occurred deleting the author.");
            bookRepositoryMock.Verify(rep => rep.DeleteAsync(muckBook), Times.Once());
        }

        [Fact]
        public async Task DeleteAsync_Invalid_ItMustReturnASuccessfulFalse()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var bookRepositoryMock = new Mock<IBookRepository>();
            var optionsMock = new Mock<IOptions<LibrarySetting>>();

            int id = 0;

            bookRepositoryMock
                .Setup(rep => rep.GetByIdAsync(id));

            var bookService = new BookService(bookRepositoryMock.Object, authorRepositoryMock.Object, optionsMock.Object);
            var result = await bookService.DeleteAsync(new Application.DTOs.Book.BookDeleteRequestDTO() { Id = id });
            Assert.True(!result.Successful, "An error occurred deleting the author.");
            bookRepositoryMock.Verify(rep => rep.GetByIdAsync(id), Times.Never());
        }

        [Fact]
        public async Task DeleteAsync_BookDoesNotExists_ItMustReturnASuccessfulFalse()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var bookRepositoryMock = new Mock<IBookRepository>();
            var optionsMock = new Mock<IOptions<LibrarySetting>>();

            int id = 1;

            bookRepositoryMock
                .Setup(rep => rep.GetByIdAsync(id))
                .ReturnsAsync((Book) null);

            var bookService = new BookService(bookRepositoryMock.Object, authorRepositoryMock.Object, optionsMock.Object);
            var result = await bookService.DeleteAsync(new Application.DTOs.Book.BookDeleteRequestDTO() { Id = id });
            Assert.True(!result.Successful, "An error occurred deleting the author.");
            bookRepositoryMock.Verify(rep => rep.GetByIdAsync(id), Times.Once());
        }

        [Fact]
        public async Task CreateAsync_GoodEscenary_ItMustReturnASuccessfulTrue()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var optionsMock = new Mock<IOptions<LibrarySetting>>();

            var libarySetting = new LibrarySetting() { MaxBooks = 5 };
            var muckBook = new Book { Id = 1, Title = "Una breve historia del tiempo", Gender = "Ciencia", PagesNumber = 194, AuthorId = 1, Year = 1988, Author = new Author { Id = 1, FullName = " Stephen Hawking" } };

            optionsMock.Setup(s => s.Value).Returns(libarySetting);

            bookRepositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(new List<Book>());

            authorRepositoryMock
                .Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(muckBook.Author);

            bookRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<Book>()))
                .ReturnsAsync(muckBook);

            var bookService = new BookService(bookRepositoryMock.Object, authorRepositoryMock.Object, optionsMock.Object);

            var result = await bookService.CreateAsync(new Application.DTOs.Book.BookSaveRequestDTO
            {
                AuthorId = muckBook.AuthorId,
                Gender = muckBook.Gender,
                PagesNumber = muckBook.PagesNumber,
                Title = muckBook.Title,
                Year = muckBook.Year
            });

            Assert.True(result.Successful, "An error occurred creating the book.");

            bookRepositoryMock.Verify(rep => rep.GetAllAsync(), Times.Once());

            authorRepositoryMock.Verify(rep => rep.GetByIdAsync(muckBook.AuthorId), Times.Once());

            bookRepositoryMock.Verify(rep => rep.CreateAsync(It.Is<Book>(x =>
                x.Title == muckBook.Title &&
                x.PagesNumber == muckBook.PagesNumber &&
                x.Year == muckBook.Year &&
                x.Gender == muckBook.Gender)), Times.Once());
        }

        [Fact]
        public async Task CreateAsync_BadRequest_ItMustReturnASuccessfulFalse()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var optionsMock = new Mock<IOptions<LibrarySetting>>();

            var libarySetting = new LibrarySetting() { MaxBooks = 5 };
            var muckBook = new Book { Id = 1, Title = "", Gender = "", PagesNumber = 0, AuthorId = 1, Year = 0 };

            optionsMock.Setup(s => s.Value).Returns(libarySetting);

            bookRepositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(new List<Book>());

            var bookService = new BookService(bookRepositoryMock.Object, authorRepositoryMock.Object, optionsMock.Object);

            var result = await bookService.CreateAsync(new Application.DTOs.Book.BookSaveRequestDTO
            {
                AuthorId = muckBook.AuthorId,
                Gender = muckBook.Gender,
                PagesNumber = muckBook.PagesNumber,
                Title = muckBook.Title,
                Year = muckBook.Year
            });

            Assert.True(!result.Successful, "It didn't fail it must fail because I sent an invalid request.");

            bookRepositoryMock.Verify(rep => rep.GetAllAsync(), Times.Never());
        }

        [Fact]
        public async Task CreateAsync_LimitReached_ItMustReturnASuccessfulFalse()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var optionsMock = new Mock<IOptions<LibrarySetting>>();

            var libarySetting = new LibrarySetting() { MaxBooks = 5 };
            var muckBook = new Book { Id = 1, Title = "Una breve historia del tiempo", Gender = "Ciencia", PagesNumber = 194, AuthorId = 1, Year = 1988, Author = new Author { Id = 1, FullName = " Stephen Hawking" } };

            optionsMock.Setup(s => s.Value).Returns(libarySetting);

            bookRepositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(new List<Book>() { new Book(),new Book(), new Book(), new Book(), new Book() });

            var bookService = new BookService(bookRepositoryMock.Object, authorRepositoryMock.Object, optionsMock.Object);

            var result = await bookService.CreateAsync(new Application.DTOs.Book.BookSaveRequestDTO
            {
                AuthorId = muckBook.AuthorId,
                Gender = muckBook.Gender,
                PagesNumber = muckBook.PagesNumber,
                Title = muckBook.Title,
                Year = muckBook.Year
            });

            Assert.True(!result.Successful, "It didn't fail it must fail because The books creation limit was reached.");

            bookRepositoryMock.Verify(rep => rep.GetAllAsync(), Times.Once());
        }
    }
}
