using System.Collections.Generic;
using System.Threading.Tasks;
namespace Moviesapi.Data
{
	public interface IMoviesDbContext
    {
        List<Movie>  Movies { get; set; }
        List<MovieStat> MoviesStats { get; set; }
        bool SaveChanges();
    }

}
