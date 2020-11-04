using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moviesapi.MovieStats.GetMovieStats;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Moviesapi.Controllers
{
    [Route("movies/stats")]
    [ApiController]
    public class MovieStatsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieStatsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<MovieStatsController>
        [HttpGet]
        public async Task<ActionResult<GetMovieStatsResponse>> Get()
        {
            var response = await _mediator.Send(new GetMovieStatsRequest());
            if (response.MovieStats!=null && response.MovieStats.Count>0)
                return response;
            else
                return NotFound();
        }

    }
}
