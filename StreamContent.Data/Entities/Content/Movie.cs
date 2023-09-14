using StreamContent.Data.Entities;
using StreamContent.Data.Entities.Enums;


    public class Movie : StreamingContentEntity
    {
        public Movie() { }

        public Movie(string title, string description, double starRating, MaturityRating maturityRating, GenreType genreType)
        : base(title, description, starRating, maturityRating, genreType)
        {

        }

        public double RunTime {get;set;}
    }
