using FluentAssertions;
using MemoryCache.Testing.Moq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MoviesApiTest
{
    [TestClass]
    public class MoviesDbContextTest
    { 
        [TestMethod]
        public void LoadDataFromCsvTest()
        { 
            var mockedCache = Create.MockedMemoryCache();
            var moviesDbContext = new Moviesapi.Data.MoviesDbContext(mockedCache, "data/metadatatest.csv", "data/statstest.csv");
            moviesDbContext.Movies.Count.Should().BeGreaterThan(0);
        } 
    }
}
