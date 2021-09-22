using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using ApplicationCore.Models;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest
    {
        // SUT (system under test)
        private MovieService _sut;


        private static List<Movie> _movies;
        private Mock<IMovieRepository> _mockMovieRepository;

        // [OneTimeSetup] in Nunit
        [TestInitialize]
        public void OneTimeSetup()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();

            _mockMovieRepository.Setup(m => m.GetHighest30GrossingMoviesAsync()).ReturnsAsync(_movies);

            _sut = new MovieService(_mockMovieRepository.Object, new MockCurrentUser());
        }

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            _movies = new List<Movie>()
            {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 12000000},
                new Movie {Id = 2, Title = "Avatar", Budget = 12000000},
                new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 12000000},
                new Movie {Id = 4, Title = "Titanic", Budget = 12000000},
                new Movie {Id = 5, Title = "Inception", Budget = 12000000},
            };
        }

        [TestMethod]
        public async Task TestListOfHighestGrowingMoviesFromFakeData()
        {
            // AAA 
            // Arrange, Act and Assert

            // Arrange is to arrange mock data, objects, methods... etc.
            // Done in Setup

            // Act
            var movies = await _sut.GetTopRevenueMoviesAsync();

            // Assert
            Assert.IsNotNull(movies);
            Assert.IsInstanceOfType(movies, typeof(IEnumerable<MovieCardResponseModel>));
            Assert.AreEqual(5, movies.Count);


        }

        public class MockCurrentUser : ICurrentUser
        {
            public int UserId => throw new NotImplementedException();

            public bool IsAuthenticated => throw new NotImplementedException();

            public string Email => throw new NotImplementedException();

            public string FullName => throw new NotImplementedException();

            public bool IsAdmin => throw new NotImplementedException();

            public bool IsSuperAdmin => throw new NotImplementedException();
        }
    }
}
