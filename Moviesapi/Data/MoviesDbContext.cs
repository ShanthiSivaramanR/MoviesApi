using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace Moviesapi.Data
{
	public class MoviesDbContext : IMoviesDbContext
	{
		private readonly IMemoryCache _cache;
		private readonly MemoryCacheEntryOptions _cacheExpirationOptions;
		public List<Movie> Movies { get; set; }
		public List<MovieStat> MoviesStats { get; set; }
		public bool SaveChanges() { return true; }
		public MoviesDbContext(IMemoryCache memoryCache, string moviesFilename = "Data/metadata.csv", string statFilename = "Data/stats.csv")
		{
			_cache = memoryCache;
			_cacheExpirationOptions = new MemoryCacheEntryOptions();
			_cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddDays(1);
			_cacheExpirationOptions.Priority = CacheItemPriority.Normal;
			LoadCsvMovies(moviesFilename);
			LoadCsvStats(statFilename);
		}
		private void LoadCsvMovies(string filePath)
		{
			List<Movie> cachedMovies = new List<Movie>(); 
			if (_cache.TryGetValue<List<Movie>>(ApiConstants.MoviesCacheKey, out cachedMovies))
			{
				Movies = cachedMovies;
			}
			else
			{
				using (TextFieldParser parser = new TextFieldParser(filePath))
				{
					Movies = new List<Movie>();
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
				_cache.Set<List<Movie>>(ApiConstants.MoviesCacheKey, Movies);
			} 
		}
		private void LoadCsvStats(string filePath)
		{
			List<MovieStat> cachedMoviestats = new List<MovieStat>(); 
			if (_cache.TryGetValue<List<MovieStat>>(ApiConstants.MovieStatsCacheKey, out cachedMoviestats))
			{
				MoviesStats = cachedMoviestats;
			}
			else
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
							WatchDurationS = ulong.Parse(fields[1])
						});
					}
					_cache.Set<List<MovieStat>>(ApiConstants.MovieStatsCacheKey, MoviesStats);
				}
			}
		}
	}

}
