using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moviesapi.GetMovie
{
    public class GetMovieResponse
    {
        public List<MovieModel> Movies { get; set; }
    }
    public class MovieModel
    { 
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public int ReleaseYear { get; set; }
    }
}
