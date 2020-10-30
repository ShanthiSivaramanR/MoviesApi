using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moviesapi.CreateMovie;
using Moviesapi.GetMovie;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Moviesapi.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/<MoviesController>/5
        [HttpGet("{movieId}")]
        public async Task<ActionResult<GetMovieResponse>> Get(int movieId)
        {
            var response = await _mediator.Send(new GetMovieRequest { MovieId = movieId } );
            if(response.IsSuccess)                
                return response;
            else
                return NotFound();
        }

        // POST api/<MoviesController>
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateMovieResponse>> PostAsync([FromBody]CreateMovieRequest request)
        {
            try
            {
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
