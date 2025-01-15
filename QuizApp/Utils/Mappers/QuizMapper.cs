using QuizApp.Models;
using QuizApp.Utils.DTOs;

namespace QuizApp.Utils.Mappers
{
    public static class QuizMapper
    {
        public static QuestionDto ToDto(Question question)
        {
            return new QuestionDto
            {
                Id = question.Id,
                Text = question.Text,
                QuestionType = question.QuestionType,
                Options = question.Options
            };
        }

        public static HighScoreDto ToDto(QuizSubmission submission, int position)
        {
            return new HighScoreDto
            {
                Position = position,
                Email = submission.Email ?? string.Empty,
                Score = submission.Score,
                SubmittedAt = submission.SubmittedAt,
                Medal = position switch
                {
                    1 => "Gold",
                    2 => "Silver",
                    3 => "Bronze",
                    _ => string.Empty
                }
            };
        }
    }
}


