using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moviesapi.CreateMovie;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Moviesapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        } 

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public string Get(int movieId)
        {
            return "value";
        }

        // POST api/<MoviesController>
        [HttpPost]
        public async Task Post([FromBody]  CreateMovieRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
            }
            catch
            {

            }
        } 
    }
}
