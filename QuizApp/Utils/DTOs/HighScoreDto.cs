namespace QuizApp.Utils.DTOs
{
    public class HighScoreDto
    {
        public int Position { get; set; }
        public required string Email { get; set; }
        public int Score { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string Medal { get; set; } = null!;
    }
}

