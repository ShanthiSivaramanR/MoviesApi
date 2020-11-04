using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moviesapi.Data;
using Moviesapi.MovieStats.GetMovieStats;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoviesApiTest
{
	[TestClass]
	class GetMovieStatsTest
	{
		private Mock<IMoviesDbContext> _dbContext;
		private GetMovieStatsHandler _movieStatsHandler;

		[TestInitialize]
		public void Setup()
		{
			_dbContext = new Mock<IMoviesDbContext>();

		}
		[TestMethod]
		public async Task moviestats_returned_orderedby_most_watched_desc_then_by_release__year_desc()
		{
			var movies = new List<Movie>
			{
				new Movie{ Duration="1", Id=1, Language="Ze",MovieId=1, ReleaseYear=2013, Title="TestMovie" },
				new Movie{ Duration="1", Id=2, Language="Ae",MovieId=2, ReleaseYear=2015, Title="TestMovie" },
				new Movie{ Duration="1", Id=3, Language="Ae",MovieId=3, ReleaseYear=2016, Title="TestMovie" },
			};
			var moviesStats = new List<MovieStat>
			{
				new MovieStat{ MovieId =1, WatchDurationS=1000 },
				new MovieStat{ MovieId =1, WatchDurationS=1000 },
				new MovieStat{ MovieId =2, WatchDurationS=1000 },
				new MovieStat{ MovieId =2, WatchDurationS=1000 },
				new MovieStat{ MovieId =2, WatchDurationS=1000 },
				new MovieStat{ MovieId =2, WatchDurationS=1000 },
				new MovieStat{ MovieId =3, WatchDurationS=1000 },
				new MovieStat{ MovieId =3, WatchDurationS=1000 }
			};
			_dbContext.Setup(x => x.Movies).Returns(movies);
			_dbContext.Setup(x => x.MoviesStats).Returns(moviesStats);
			_movieStatsHandler = new GetMovieStatsHandler(_dbContext.Object);
			var result = await _movieStatsHandler.Handle(new GetMovieStatsRequest(), CancellationToken.None);
			result.MovieStats[0].MovieId.Should().Be(2);//most watched desc
			result.MovieStats[1].MovieId.Should().Be(3);//most recent
			result.MovieStats[2].MovieId.Should().Be(1);//most watched desc


		}
	}
}
