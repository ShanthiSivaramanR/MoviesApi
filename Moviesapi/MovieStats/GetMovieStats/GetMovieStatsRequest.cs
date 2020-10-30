using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moviesapi.MovieStats.GetMovieStats
{
    public class GetMovieStatsRequest : IRequest<GetMovieStatsResponse>
    {
    }
}
