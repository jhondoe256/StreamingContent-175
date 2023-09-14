using StreamContent.Data.Entities;
using StreamContent.Data.Entities.Enums;
using StreamContent.Data.Entities.Content;

    public class Show : StreamingContentEntity
    {
        public Show() { }

        public Show(string title, string description, double starRating, MaturityRating maturityRating, GenreType genreType)
        : base(title, description, starRating, maturityRating, genreType)
        {

        }

        public Show(string title, string description, double starRating, MaturityRating maturityRating, GenreType genreType,
        List<Episode> episodes)
        : base(title, description, starRating, maturityRating, genreType)
        {
            Episodes = episodes;
        }

        public Show(List<Episode> episodes)
        {
            Episodes = episodes;
        }

        public List<Episode> Episodes { get; set; } = new List<Episode>();
        public int SeasonCount { get; set; }
        public int EpisodeCount { get { return Episodes.Count; } }
        public double AverageRunTime
        {
            get
            {
                //*             using linq  Language Inetgrated Query
                return Episodes.Average(c => c.RunTime);
            }
        }
    }
