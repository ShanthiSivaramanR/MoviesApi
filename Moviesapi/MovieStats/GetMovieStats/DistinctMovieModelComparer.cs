using System.Collections.Generic;

namespace Moviesapi.MovieStats.GetMovieStats
{
    public class DistinctMovieModelComparer : IEqualityComparer<MovieStatResopnseModel>
    {
        public bool Equals(MovieStatResopnseModel x, MovieStatResopnseModel y)
        {
            return x.MovieId == y.MovieId;
        }

        public int GetHashCode(MovieStatResopnseModel obj)
        {
            return obj.MovieId.GetHashCode();
        }
    }
}
