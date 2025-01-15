namespace QuizApp.Utils.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required string QuestionType { get; set; }
        public required List<string> Options { get; set; }
    }
}


