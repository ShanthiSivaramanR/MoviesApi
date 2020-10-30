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
        public float AverageWatchDuration { get; set; }
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }
    }
    public class DistinctMovieModelComparer : IEqualityComparer<MovieStatResopnseModel>
    {
        public bool Equals(MovieStatResopnseModel x, MovieStatResopnseModel y)
        {
            return x.MovieId == y.MovieId &&
                x.AverageWatchDuration == y.AverageWatchDuration &&
                x.Watches == y.Watches&&
                x.ReleaseYear == y.ReleaseYear;
        }

        public int GetHashCode(MovieStatResopnseModel obj)
        {
            return obj.MovieId.GetHashCode() ^ 
                obj.AverageWatchDuration.GetHashCode() ^
                obj.Watches.GetHashCode() ^
                obj.ReleaseYear.GetHashCode();
        }
    }
}
