using QuizApp.Models;
using System.Collections.Generic;

namespace QuizApp.Services
{
    public interface IScoringService
    {
        int CalculateScore(Question question, List<string> answers);
    }
}