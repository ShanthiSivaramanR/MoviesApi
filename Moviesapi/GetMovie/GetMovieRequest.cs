using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moviesapi.GetMovie
{
    public class GetMovieRequest : IRequest<GetMovieResponse>
    {
        public int MovieId { get; set; }
    }
}
