using QuizApp.Models;
using QuizApp.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace QuizApp.Tests.Services
{
    public class ScoringServiceTests
    {
        private readonly ScoringService _scoringService;

        public ScoringServiceTests()
        {
            _scoringService = new ScoringService();
        }

        [Fact]
        public void CalculateScore_SingleCorrectAnswer_ShouldReturnFullScore()
        {
            // Arrange
            var question = new Question
            {
                QuestionType = "Single",
                CorrectAnswers = new List<string> { "1" }
            };
            var answers = new List<string> { "1" };

            // Act
            var score = _scoringService.CalculateScore(question, answers);

            // Assert
            Assert.Equal(100, score);
        }

        [Fact]
        public void CalculateScore_SingleIncorrectAnswer_ShouldReturnZero()
        {
            // Arrange
            var question = new Question
            {
                QuestionType = "Single",
                CorrectAnswers = new List<string> { "1" }
            };
            var answers = new List<string> { "2" };

            // Act
            var score = _scoringService.CalculateScore(question, answers);

            // Assert
            Assert.Equal(0, score);
        }

        [Fact]
        public void CalculateScore_MultipleCorrectAnswers_ShouldReturnFullScore()
        {
            // Arrange
            var question = new Question
            {
                QuestionType = "Multiple",
                CorrectAnswers = new List<string> { "0", "1", "2" }
            };
            var answers = new List<string> { "0", "1", "2" };

            // Act
            var score = _scoringService.CalculateScore(question, answers);

            // Assert
            Assert.Equal(100, score);
        }

        [Fact]
        public void CalculateScore_MultiplePartialCorrectAnswers_ShouldReturnPartialScore()
        {
            // Arrange
            var question = new Question
            {
                QuestionType = "Multiple",
                CorrectAnswers = new List<string> { "0", "1", "2" }
            };
            var answers = new List<string> { "0", "1" };

            // Act
            var score = _scoringService.CalculateScore(question, answers);

            // Assert
            Assert.Equal(67, score); // 100 / 3 * 2 = 66.67, rounded up to 67
        }

        [Fact]
        public void CalculateScore_MultipleIncorrectAnswers_ShouldReturnZero()
        {
            // Arrange
            var question = new Question
            {
                QuestionType = "Multiple",
                CorrectAnswers = new List<string> { "0", "1", "2" }
            };
            var answers = new List<string> { "3", "4" };

            // Act
            var score = _scoringService.CalculateScore(question, answers);

            // Assert
            Assert.Equal(0, score);
        }

        [Fact]
        public void CalculateScore_TextCorrectAnswer_ShouldReturnFullScore()
        {
            // Arrange
            var question = new Question
            {
                QuestionType = "Text",
                CorrectAnswers = new List<string> { "quiz" }
            };
            var answers = new List<string> { "quiz" };

            // Act
            var score = _scoringService.CalculateScore(question, answers);

            // Assert
            Assert.Equal(100, score);
        }

        [Fact]
        public void CalculateScore_TextCaseInsensitiveCorrectAnswer_ShouldReturnFullScore()
        {
            // Arrange
            var question = new Question
            {
                QuestionType = "Text",
                CorrectAnswers = new List<string> { "quiz" }
            };
            var answers = new List<string> { "QUIZ" };

            // Act
            var score = _scoringService.CalculateScore(question, answers);

            // Assert
            Assert.Equal(100, score);
        }

        [Fact]
        public void CalculateScore_TextIncorrectAnswer_ShouldReturnZero()
        {
            // Arrange
            var question = new Question
            {
                QuestionType = "Text",
                CorrectAnswers = new List<string> { "quiz" }
            };
            var answers = new List<string> { "test" };

            // Act
            var score = _scoringService.CalculateScore(question, answers);

            // Assert
            Assert.Equal(0, score);
        }

        [Fact]
        public void CalculateScore_InvalidQuestionType_ShouldReturnZero()
        {
            // Arrange
            var question = new Question
            {
                QuestionType = "InvalidType",
                CorrectAnswers = new List<string> { "1" }
            };
            var answers = new List<string> { "1" };

            // Act
            var score = _scoringService.CalculateScore(question, answers);

            // Assert
            Assert.Equal(0, score);
        }
    }
}
