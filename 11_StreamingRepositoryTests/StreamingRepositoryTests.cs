using StreamContent.Data.Entities.Content;
using StreamContent.Data.Entities.Enums;
using StreamContent.Repository;

namespace _11_StreamingRepositoryTests;

public class StreamingRepositoryTests
{
    private StreamingRepository _repo;
    private Movie _movieA;
    private Movie _movieB;
    private Show _showA;
    private List<Episode> _TRIEpisodes;
    private Episode _episodeA;
    private Episode _episodeA1;
    private Show _showTestA;

    public StreamingRepositoryTests()
    {
        _repo = new StreamingRepository();

        _movieA = new Movie("Bad Boys", "Cop Film", 10, MaturityRating.R, GenreType.Action);

        _movieB = new Movie("50 First Dates", "Romance Flick Film", 10, MaturityRating.R, GenreType.RomCom);

        _TRIEpisodes = new List<Episode>
        {
            new Episode
            {
                Title = "The Price is Right Ep.1",
                RunTime = .5d,
                SeasonNumber = 1
            },
            new Episode
            {
                Title = "The Price is Right Ep.2",
                RunTime = .5d,
                SeasonNumber = 1
            }
        };

        _showA = new Show("The Price is Right", "Family Show, where the elderly hit it BIG!!!", 10, MaturityRating.G, GenreType.Drama, _TRIEpisodes);

        //* Add the items to the database:
        _repo.AddContentToDb(_showA);
        _repo.AddContentToDb(_movieA);
        _repo.AddContentToDb(_movieB);
    }

    [Fact]
    public void TotalCount()
    {
        //* Act
        int expected = 2;
        int actual = _showA.EpisodeCount;

        //* Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AverageShowTime()
    {
        double expected = .5d;
        double actual = _showA.AverageRunTime;

        //* Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetShowTitle_ShouldGiveMeAShow()
    {
        //* Act
        Show retrivedShow = _repo.GetShowByTitle("The Price Is RIGHT");

        Show expected = _showA;
        Show actual = retrivedShow;

        //* Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetMovieByTitle_ShouldGiveMeAMovie()
    {
        //* Act
        Movie retrivedMovie = _repo.GetMovieByTitle("BaD BoYs");

        Movie expected = _movieA;
        Movie actual = retrivedMovie;

        //* Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetAllShows_ShouldGiveMeAListOfShows()
    {
        //* Act
        List<Show> retrievedShows = _repo.GetAllShows();

        int expectedCount = 1;
        int actual = retrievedShows.Count;

        //* Assert
        Assert.Equal(expectedCount, actual);
    }

    [Fact]
    public void GetAllMovies_ShouldReturnAListOfMovies()
    {
        //* Act
        List<Movie> retrievedMoives = _repo.GetAllMovies();

        int expectedCount = 2;
        int actual = retrievedMoives.Count;

        //* Assert
        Assert.Equal(expectedCount,actual);
    }
}