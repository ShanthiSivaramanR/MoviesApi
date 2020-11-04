using MediatR;

namespace Moviesapi.GetMovie
{
    public class GetMovieRequest : IRequest<GetMovieResponse>
    {
        public int MovieId { get; set; }
    }
}
