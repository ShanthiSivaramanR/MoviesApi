using System.Collections.Generic;

namespace Moviesapi.MovieStats.GetMovieStats
{
    public class DistinctMovieModelComparer : IEqualityComparer<MovieStatResopnseModel>
    {
        public bool Equals(MovieStatResopnseModel x, MovieStatResopnseModel y)
        {
            return x.MovieId == y.MovieId &&
                x.AverageWatchDurationS == y.AverageWatchDurationS &&
                x.Watches == y.Watches&&
                x.ReleaseYear == y.ReleaseYear;
        }

        public int GetHashCode(MovieStatResopnseModel obj)
        {
            return obj.MovieId.GetHashCode() ^ 
                obj.AverageWatchDurationS.GetHashCode() ^
                obj.Watches.GetHashCode() ^
                obj.ReleaseYear.GetHashCode();
        }
    }
}
