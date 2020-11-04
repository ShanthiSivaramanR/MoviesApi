using MediatR;
using Moviesapi.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Moviesapi.MovieStats.GetMovieStats
{
	public class GetMovieStatsHandler : IRequestHandler<GetMovieStatsRequest, GetMovieStatsResponse>
	{
		private readonly IMoviesDbContext _moviesDbContext;

		public GetMovieStatsHandler(IMoviesDbContext moviesDbContext)
		{
			_moviesDbContext = moviesDbContext ?? throw new ArgumentNullException(nameof(moviesDbContext));
		}
		public Task<GetMovieStatsResponse> Handle(GetMovieStatsRequest request, CancellationToken cancellationToken)
		{
			var optStat = _moviesDbContext.MoviesStats.GroupBy(i => i.MovieId)
			.Select(g => new
			{
				MovieId = g.Key,
				Count = g.Count(),
				Total = g.Sum(i => (float)i.WatchDurationS),
				Average = (int)TimeSpan.FromMilliseconds(g.Average(i => (float)i.WatchDurationS)).TotalSeconds
			});

			var movieStats = _moviesDbContext.Movies
				.Join(optStat,
					mov => mov.MovieId,
					stat => stat.MovieId,
					(mov, stat) => new { Movie = mov, optStat = stat })
				.Select(grp => new MovieStatResopnseModel
				{
					MovieId = grp.Movie.MovieId,
					ReleaseYear = grp.Movie.ReleaseYear,
					Title = grp.Movie.Title,
					Watches = grp.optStat.Count,
					AverageWatchDurationS = grp.optStat.Average
				})
				.OrderByDescending(x => x.Watches).ThenByDescending(x => x.ReleaseYear)
				.Distinct(new DistinctMovieModelComparer())
				.ToList();
			return Task.FromResult(new GetMovieStatsResponse { MovieStats = movieStats });
		}
	}
}
