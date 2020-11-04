using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moviesapi.CreateMovie;
using Moviesapi.Data;
using Moviesapi.GetMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoviesApiTest
{
	[TestClass]
	public class GetMoviesTest
	{
		private Mock<IMoviesDbContext> _dbContext;
		private GetMovieHandler _movieHandler;

		[TestInitialize]
		public void Setup()
		{
			_dbContext = new Mock<IMoviesDbContext>();

		}
		[TestMethod]
		public async Task movies_are_returned_orderedby_language()
		{
			var movieID = 1;
			var unOrderedLanguageMovies = new List<Movie>
			{
				new Movie{ Duration="1", Id=1, Language="Ze",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="1", Id=2, Language="Ae",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="1", Id=3, Language="Se",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="1", Id=4, Language="Ce",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="1", Id=5, Language="En",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" }
			};
			_dbContext.Setup(x => x.Movies).Returns(unOrderedLanguageMovies);
			_movieHandler = new GetMovieHandler(_dbContext.Object);
			var result = await _movieHandler.Handle(new GetMovieRequest { MovieId = movieID }, CancellationToken.None);
			result.Movies.Should().BeInAscendingOrder(x => x.Language);
		}
		[TestMethod]
		public async Task latest_movies_records_are_returned_forlangiage()
		{
			var movieID = 1;
			var latestMovieRecord = new List<Movie>
			{
				new Movie{ Duration="1", Id=1, Language="Ze",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="1", Id=2, Language="Ae",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="1", Id=3, Language="Et",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="1", Id=4, Language="Ce",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="9", Id=5, Language="Et",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" }
			};
			_dbContext.Setup(x => x.Movies).Returns(latestMovieRecord);
			_movieHandler = new GetMovieHandler(_dbContext.Object);
			var result = await _movieHandler.Handle(new GetMovieRequest { MovieId = movieID }, CancellationToken.None);
			result.Movies.Count.Should().Be(4);
			result.Movies.Exists(x => x.Duration.Equals("9")).Should().BeTrue();
		}
		[TestMethod]
		public async Task invalid_metadata_movies_are_not_returned()
		{
			var movieID = 1;
			var latestMovieRecord = new List<Movie>
			{
				new Movie{ Duration="1", Id=1, MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="1", Id=2, Language="Ae",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="1", Id=3, Language="Et",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="1", Id=4, Language="Ce",MovieId=movieID, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="9", Id=5, Language="E2",MovieId=movieID,  Title="TestMovie" }
			};
			_dbContext.Setup(x => x.Movies).Returns(latestMovieRecord);
			_movieHandler = new GetMovieHandler(_dbContext.Object);
			var result = await _movieHandler.Handle(new GetMovieRequest { MovieId = movieID }, CancellationToken.None);
			result.Movies.Count.Should().Be(3); 
		}
	}
}

