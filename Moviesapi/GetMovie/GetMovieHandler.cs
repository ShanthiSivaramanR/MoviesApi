using MediatR;
using Moviesapi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var moviesDistinct = _moviesDbContext.Movies
                    .Where(c => c.MovieId == request.MovieId)
                    .GroupBy(c => c.Language)
                    .Select(g => g.OrderByDescending(c => c.Id).First())
                    .Select(movie => new MovieModel
                    {
                        Duration = movie.Duration,
                        Language = movie.Language,
                        MovieId = movie.MovieId,
                        ReleaseYear = movie.ReleaseYear,
                        Title = movie.Title
                    }).ToList();
            return Task.FromResult(new GetMovieResponse { Movies = moviesDistinct });
        }
    }
}
