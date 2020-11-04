namespace Moviesapi.MovieStats.GetMovieStats
{
    public class MovieStatResopnseModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public float AverageWatchDurationS { get; set; }
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }
    }
}
