using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace MoviesApiTest
{
    [TestClass]
    public class MoviesDbContextTest
    {
        [TestMethod]
        public void LoadDataFromCsvTest()
        {
            var moviesDbContext = new Moviesapi.Data.MoviesDbContext("data/metadatatest.csv", "data/statstest.csv");
            moviesDbContext.Movies.Count.Should().BeGreaterThan(0);
        } 
    }
}
