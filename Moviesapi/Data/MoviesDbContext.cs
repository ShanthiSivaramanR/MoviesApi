using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
namespace Moviesapi.Data
{
    public class MoviesDbContext : IMoviesDbContext
    {
        public List<Movie> Movies { get; set; }
        public List<MovieStat> MoviesStats { get; set; }
        public bool SaveChanges() { return true; }

        public MoviesDbContext(string moviesFilename = "Data/metadata.csv", string statFilename = "Data/stats.csv")
        {
            LoadCsvMovies(moviesFilename);
            LoadCsvStats(statFilename);
        }
        private void LoadCsvMovies(string filePath)
        {
            Movies = new List<Movie>();
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                if (!parser.EndOfData)
                {
                    parser.ReadLine();
                }
                while (!parser.EndOfData)
                {
                    //Id,MovieId,Title,Language,Duration,ReleaseYear
                    //1,3,Elysium,AR,01:49:00,2013
                    string[] fields = parser.ReadFields();
                    Movies.Add(new Movie
                    {
                        Id = int.Parse(fields[0]),
                        MovieId = int.Parse(fields[1]),
                        Title = fields[2].ToString(),
                        Language = fields[3].ToString(),
                        Duration = fields[4].ToString(),
                        ReleaseYear = int.Parse(fields[5])
                    });
                }
            }
        }
        private void LoadCsvStats(string filePath)
        {

            MoviesStats = new List<MovieStat>();
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                if (!parser.EndOfData)
                {
                    parser.ReadLine();
                }
                while (!parser.EndOfData)
                {
                    //Id,MovieId,Title,Language,Duration,ReleaseYear
                    //1,3,Elysium,AR,01:49:00,2013
                    string[] fields = parser.ReadFields();
                    MoviesStats.Add(new MovieStat
                    {
                        MovieId = int.Parse(fields[0]),
                        AverageWatchDurationS = ulong.Parse(fields[1])
                    });
                }
            }
        }
    }

    public interface IMoviesDbContext
    {
        List<Movie> Movies { get; set; }
        List<MovieStat> MoviesStats { get; set; }
        bool SaveChanges();
    }

}
