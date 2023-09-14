using StreamContent.Data.Entities.Enums;

namespace StreamContent.Data.Entities
{
    public class StreamingContentEntity
    {
          public StreamingContentEntity() { }

        public StreamingContentEntity(
            string title,
            string description,
            double starRating,
            MaturityRating maturityRating,
            GenreType genreType
            )
        {
            Title = title;
            Description = description;
            StarRating = starRating;
            MaturityRating = maturityRating;
            GenreType = genreType;
        }

        //* This is our unique identifier   (Id)
        public string Title { get; set; }
        public string Description { get; set; }
        public double StarRating { get; set; }
        public MaturityRating MaturityRating { get; set; }
        public GenreType GenreType { get; set; }
        public bool IsFamilyFriendly
        {
            get
            {
                switch (MaturityRating)
                {
                    case MaturityRating.G:
                    case MaturityRating.PG:
                    case MaturityRating.TV_Y:
                    case MaturityRating.TV_G:
                    case MaturityRating.TV_PG:
                        return true;
                    case MaturityRating.R:
                    case MaturityRating.PG_13:
                    case MaturityRating.TV_14:
                    case MaturityRating.NC_17:
                    case MaturityRating.TV_MA:
                        return false;
                    default:
                    return false;
                }
            }
        }
    }
}