namespace RateMyP.WebApp.Models.Leaderboard
    {
    public class LeaderboardEntry
        {
        public int AllTimePosition { get; set; }
        public int AllTimeRatingCount { get; set; }
        public double AllTimeAverage { get; set; }
        public double AllTimeScore { get; set; }
        public int ThisYearPosition { get; set; }
        public int ThisYearRatingCount { get; set; }
        public double ThisYearAverage { get; set; }
        public double ThisYearScore { get; set; }
        }
    }
