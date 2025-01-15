using QuizApp.Models;
using System;
using System.Collections.Generic;

namespace QuizApp.Services
{
    public class ScoringService : IScoringService
    {
        public int CalculateScore(Question question, List<string> answers)
        {
            switch (question.QuestionType)
            {
                case "Single":
                    return question.CorrectAnswers.Contains(answers[0]) ? 100 : 0;

                case "Multiple":
                    var correctCount = answers.Count(a => question.CorrectAnswers.Contains(a));
                    return (int)Math.Ceiling(100.0 / question.CorrectAnswers.Count * correctCount);

                case "Text":
                    return question.CorrectAnswers.Contains(answers[0], StringComparer.OrdinalIgnoreCase) ? 100 : 0;

                default:
                    return 0;
            }
        }
    }
}