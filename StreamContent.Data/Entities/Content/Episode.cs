namespace StreamContent.Data.Entities.Content
{
    public class Episode
    {
        public Episode(){}
        
        public Episode(string title, double runTime,int seasonNumber)
        {
            Title = title;
            RunTime = runTime;
            SeasonNumber = seasonNumber;
        }

        public string Title { get; set; } = string.Empty;
        public double RunTime { get; set; }
        public int SeasonNumber { get; set; }
    }
}