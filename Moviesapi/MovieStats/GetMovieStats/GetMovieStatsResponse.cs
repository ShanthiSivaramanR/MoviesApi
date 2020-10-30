using Moviesapi.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moviesapi.MovieStats.GetMovieStats
{
    public class GetMovieStatsResponse : IResponse
    {
        public List<MovieStatResopnseModel> MovieStats { get; set; }

        public bool IsSuccess => !(MovieStats is null) && MovieStats.Count() > 0;
    }

    public class MovieStatResopnseModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public float AverageWatchDurationS { get; set; }
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }
    }
}
