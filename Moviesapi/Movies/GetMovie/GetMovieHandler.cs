using MediatR;
using Moviesapi.Data;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Moviesapi.GetMovie
{
	public class GetMovieHandler : IRequestHandler<GetMovieRequest, GetMovieResponse>
	{
		private readonly IMoviesDbContext _moviesDbContext;

		public GetMovieHandler(IMoviesDbContext moviesDbContext)
		{
			_moviesDbContext = moviesDbContext ?? throw new ArgumentNullException(nameof(moviesDbContext));
		}
		public Task<GetMovieResponse> Handle(GetMovieRequest request, CancellationToken cancellationToken)
		{
			var defaultLanguage = _moviesDbContext.Movies
								.FirstOrDefault(x => x.MovieId == request.MovieId
									&& !string.IsNullOrEmpty(x.Language) 
									&& x.Language.Equals(ApiConstants.DefaultLanguageCode, StringComparison.InvariantCultureIgnoreCase));
			var defaultLanguageTitle = defaultLanguage != null ? defaultLanguage.Title : string.Empty ;

			var moviesDistinct = _moviesDbContext.Movies
					.Where(c => c.MovieId == request.MovieId
						&& !string.IsNullOrEmpty(c.Duration)
						&& !string.IsNullOrEmpty(c.Language)
						&& !string.IsNullOrEmpty(c.Title)
						&& c.ReleaseYear > 1800)
					.GroupBy(c => c.Language)
					.Select(g => g.OrderByDescending(c => c.Id).First())
					.Select(movie => new MovieModel
					{
						Duration = movie.Duration,
						Language = movie.Language,
						MovieId = movie.MovieId,
						ReleaseYear = movie.ReleaseYear,
						Title = movie.Language.Equals("EN", StringComparison.InvariantCultureIgnoreCase)
							? movie.Title
							: $"{movie.Title} ({defaultLanguageTitle})"

					})
					.OrderBy(o => o.Language)
					.ToList();
			return Task.FromResult(new GetMovieResponse { Movies = moviesDistinct });
		}
	}
}
