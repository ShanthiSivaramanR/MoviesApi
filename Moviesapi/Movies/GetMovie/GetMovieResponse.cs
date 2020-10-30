using Microsoft.AspNetCore.ResponseCaching;
using Moviesapi.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moviesapi.GetMovie
{
    public class GetMovieResponse : IResponse
    {
        public List<MovieModel> Movies { get; set; }
        public bool IsSuccess => !(Movies is null) && Movies.Count() > 0;
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
