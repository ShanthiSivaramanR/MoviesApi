using MediatR;
using Moviesapi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moviesapi.CreateMovie
{
    public class CreateMovieHandler : IRequestHandler<CreateMovieRequest, CreateMovieResponse>
    {
        private readonly IMoviesDbContext _moviesDbContext;

        public CreateMovieHandler(IMoviesDbContext moviesDbContext)
        {
            _moviesDbContext = moviesDbContext ?? throw new ArgumentNullException(nameof(moviesDbContext));
        }
        public async Task<CreateMovieResponse> Handle(CreateMovieRequest request, CancellationToken cancellationToken)
        {
            var id = _moviesDbContext.Movies.Count() + 1;
            _moviesDbContext.Movies.Add(new Movie
            {
                Duration = request.Duration,
                Id = id,
                Language = request.Language,
                MovieId = request.MovieId,
                ReleaseYear = request.ReleaseYear,
                Title = request.Title
            });
            var result = _moviesDbContext.SaveChanges();
            return new CreateMovieResponse { Message = result.ToString() };
        }
    }
}
