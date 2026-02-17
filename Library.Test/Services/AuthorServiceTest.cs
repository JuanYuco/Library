using Library.Application.Services;
using Library.Domain.Enitities;
using Library.Domain.Ports;
using Moq;

namespace Library.Test.Services
{
    public class AuthorServiceTest
    {
        [Fact]
        public async Task GetAllAsync_ItMustReturnAllElements()
        {
            var mockData = new List<Author>
            {
                new Author { Id = 1, FullName = "Juan Fernando Yuco", BirthDate = DateTime.Now.AddYears(-27), BornCity = "Cali", Email = "juanfernandoyuco@gmail.com" },
                new Author { Id = 2, FullName = "Leonardo Aedo", BirthDate = DateTime.Now.AddYears(-27), BornCity = "Cali", Email = "leoaedo@gmail.com" },
                new Author { Id = 3, FullName = "Melissa Restrepo", BirthDate = DateTime.Now.AddYears(-26), BornCity = "Bogotá", Email = "Melissa@gmail.com" },
            };

            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var bookRepositoryMock = new Mock<IBookRepository>();
            authorRepositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(mockData);

            var authorService = new AuthorService(authorRepositoryMock.Object, bookRepositoryMock.Object);

            var result = await authorService.GetAllAsync(new Application.DTOs.Author.AuthorCollectionRequestDTO());
            Assert.True(result.Successful, "An error ocurrred consulting the information.");

            var authors = result.Authors;
            Assert.Equal(mockData.Count, authors.Count);
            foreach (var mockItem in mockData)
            {
                var author = authors.Where(x => x.Id == mockItem.Id).FirstOrDefault();

                Assert.NotNull(author);
                Assert.Equal(mockItem.FullName, author.FullName);
                Assert.Equal(mockItem.Email, author.Email);
                Assert.Equal(mockItem.BornCity, author.BornCity);
                Assert.Equal(mockItem.BirthDate, author.BirthDate);
            }

            authorRepositoryMock.Verify(rep => rep.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllMinifiedAsync_ItMustReturnAllElements()
        {
            var mockData = new List<Author>
            {
                new Author { Id = 1, FullName = "Juan Fernando Yuco", Email = "juanfernandoyuco@gmail.com" },
                new Author { Id = 2, FullName = "Leonardo Aedo", Email = "leoaedo@gmail.com" },
                new Author { Id = 3, FullName = "Melissa Restrepo", Email = "Melissa@gmail.com" },
            };

            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var bookRepositoryMock = new Mock<IBookRepository>();
            authorRepositoryMock
                .Setup(rep => rep.GetAllMinifiedAsync())
                .ReturnsAsync(mockData);

            var authorService = new AuthorService(authorRepositoryMock.Object, bookRepositoryMock.Object);

            var result = await authorService.GetAllMinifiedAsync(new Application.DTOs.Author.AuthorMinifiedCollectionRequestDTO());
            Assert.True(result.Successful, "An error ocurrred consulting the information.");

            var authors = result.Authors;
            Assert.Equal(mockData.Count, authors.Count);
            foreach (var mockItem in mockData)
            {
                var author = authors.Where(x => x.Id == mockItem.Id).FirstOrDefault();

                Assert.NotNull(author);
                Assert.Equal($"{mockItem.FullName} ({mockItem.Email})", author.Description);
            }

            authorRepositoryMock.Verify(rep => rep.GetAllMinifiedAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_GoodEscenary_ItMustReturnASuccessfulTrue()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var bookRepositoryMock = new Mock<IBookRepository>();

            var mockAuthor = new Author { Id = 1, FullName = "Juan Fernando Yuco", Email = "juanfernandoyuco@gmail.com" };

            authorRepositoryMock
                .Setup(rep => rep.GetByIdAsync(mockAuthor.Id))
                .ReturnsAsync(mockAuthor);

            authorRepositoryMock
                .Setup(rep => rep.DeleteAsync(mockAuthor));

            bookRepositoryMock
                .Setup(rep => rep.GetByAuthorIdAsync(mockAuthor.Id))
                .ReturnsAsync(new List<Book>());

            var authorService = new AuthorService(authorRepositoryMock.Object, bookRepositoryMock.Object);
            var result = await authorService.DeleteAsync(new Application.DTOs.Author.AuthorDeleteRequestDTO() { AuthorId = mockAuthor.Id });
            Assert.True(result.Successful, "An error occurred deleting the author.");
            authorRepositoryMock.Verify(rep => rep.DeleteAsync(mockAuthor), Times.Once());
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ItMustReturnASuccessfulFalse()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var bookRepositoryMock = new Mock<IBookRepository>();

            int id = 0;
            authorRepositoryMock
                .Setup(rep => rep.GetByIdAsync(id));

            var authorService = new AuthorService(authorRepositoryMock.Object, bookRepositoryMock.Object);
            var result = await authorService.DeleteAsync(new Application.DTOs.Author.AuthorDeleteRequestDTO() { AuthorId = id });
            Assert.True(!result.Successful, "It didn't fail and it must because it received an invalid id.");
            authorRepositoryMock.Verify(rep => rep.GetByIdAsync(id), Times.Never());
        }

        [Fact]
        public async Task DeleteAsync_AuthorDoesNotExist_ItMustReturnASuccessfulFalse()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var bookRepositoryMock = new Mock<IBookRepository>();

            int id = 1;

            authorRepositoryMock
                .Setup(rep => rep.GetByIdAsync(id))
                .ReturnsAsync((Author)null);

            bookRepositoryMock
                .Setup(rep => rep.GetByAuthorIdAsync(id))
                .ReturnsAsync(new List<Book>());

            var authorService = new AuthorService(authorRepositoryMock.Object, bookRepositoryMock.Object);
            var result = await authorService.DeleteAsync(new Application.DTOs.Author.AuthorDeleteRequestDTO() { AuthorId = id });
            Assert.True(!result.Successful, "It didn't fail and it must because it received an invalid id.");
            bookRepositoryMock.Verify(rep => rep.GetByAuthorIdAsync(id), Times.Never());
        }

        [Fact]
        public async Task CreateAsync_GoodEscenary_ItMustReturnASuccessfulTrue()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var bookRepositoryMock = new Mock<IBookRepository>();

            var mockAuthor = new Author { Id = 1, FullName = "Juan Fernando Yuco", BirthDate = DateTime.Now.AddYears(-27), BornCity = "Cali", Email = "juanfernandoyuco@gmail.com" };

            authorRepositoryMock
                .Setup(rep => rep.GetByEmailAsync(mockAuthor.Email))
                .ReturnsAsync((Author) null);

            authorRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<Author>()))
                .ReturnsAsync(mockAuthor);

            var authorService = new AuthorService(authorRepositoryMock.Object, bookRepositoryMock.Object);

            var result = await authorService.CreateAsync(new Application.DTOs.Author.AuthorSaveRequestDTO
            {
                FullName = mockAuthor.FullName,
                BirthDate = mockAuthor.BirthDate,
                Email = mockAuthor.Email,
                BornCity = mockAuthor.BornCity
            });

            Assert.True(result.Successful, "An error occurred creating the author.");
            authorRepositoryMock.Verify(rep => rep.CreateAsync(It.Is<Author>(x => 
                x.FullName == mockAuthor.FullName &&
                x.BirthDate == mockAuthor.BirthDate &&
                x.Email == mockAuthor.Email &&
                x.BornCity == mockAuthor.BornCity)), Times.Once());
        }

        [Fact]
        public async Task CreateAsync_BadRequest_ItMustReturnASuccessfulTrue()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var bookRepositoryMock = new Mock<IBookRepository>();

            var mockAuthor = new Author { Id = 1, FullName = "", BirthDate = DateTime.Now.AddYears(1), BornCity = "", Email = "juanfernandoyucocom" };

            authorRepositoryMock
                .Setup(rep => rep.GetByEmailAsync(mockAuthor.Email))
                .ReturnsAsync((Author)null);

            authorRepositoryMock
                .Setup(rep => rep.CreateAsync(It.IsAny<Author>()))
                .ReturnsAsync(mockAuthor);

            var authorService = new AuthorService(authorRepositoryMock.Object, bookRepositoryMock.Object);

            var result = await authorService.CreateAsync(new Application.DTOs.Author.AuthorSaveRequestDTO
            {
                FullName = mockAuthor.FullName,
                BirthDate = mockAuthor.BirthDate,
                Email = mockAuthor.Email,
                BornCity = mockAuthor.BornCity
            });

            Assert.True(!result.Successful, "It didn't fail and it must because it received an invalid request.");
            authorRepositoryMock.Verify(rep => rep.CreateAsync(It.Is<Author>(x =>
                x.FullName == mockAuthor.FullName &&
                x.BirthDate == mockAuthor.BirthDate &&
                x.Email == mockAuthor.Email &&
                x.BornCity == mockAuthor.BornCity)), Times.Never());
        }
    }
}
