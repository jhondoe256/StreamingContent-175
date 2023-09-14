using StreamContent.Data.Entities;
using StreamContent.Data.Entities.Content;

namespace StreamContent.Repository
{
    public class StreamingRepository : StreamContentRepository
    {
        public Show GetShowByTitle(string title)
        {
            foreach(StreamingContentEntity content in _contentDb)
            {
                if(content.Title.ToLower() == title.ToLower() && content.GetType() == typeof(Show))
                {
                    return (Show)content;
                }
            }
            return null; //* return nothing
        }

        public List<Show> GetAllShows()
        {
            List<Show> allShows = new List<Show>();

            foreach(var content in _contentDb)
            {
                if(content is Show)
                {
                    allShows.Add((Show)content);
                }
            }
            return allShows;

            //* return _contentDb.Where(stContentEntity => stContentEntity is Show)
            //*                 .Select(stContentEntity => (Show)stContentEntity)
            //*                 .ToList();
        }

        public Movie GetMovieByTitle(string title)
        {
            foreach(StreamingContentEntity content in _contentDb)
            {
                if(content.Title.ToLower() == title.ToLower() && content.GetType() == typeof(Movie))
                {
                    return (Movie)content;
                }
            }
            return null;

            //todo: using L.I.N.Q
            //* var movie = _contentDb.FirstOrDefault(stContentEntity =>
            //*                                      stContentEntity.Title.ToLower() == title.ToLower() &&
            //*                                      stContentEntity is Movie);
        }

        public List<Movie> GetAllMovies()
        {
            List<Movie> allMovies = new List<Movie>();

            foreach(var content in _contentDb)
            {
                if(content is Movie)
                {
                    allMovies.Add((Movie)content);
                }

            }
            return allMovies;

            //* var movies = _contentDb.Where(stContentEntity => stContentEntity is Movie).Select(stContentEntity => (Movie)stContentEntity).ToList();
            //* return movies;
        }

    
    }
}