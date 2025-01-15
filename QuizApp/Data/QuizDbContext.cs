using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizSubmission> QuizSubmissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Questions
            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    Id = -1,
                    Text = "Which planet is known as the Red Planet?",
                    QuestionType = "Single",
                    Options = new List<string> { "Earth", "Mars", "Jupiter", "Venus" },
                    CorrectAnswers = new List<string> { "1" }
                },
                new Question
                {
                    Id = -2,
                    Text = "What is the capital of France?",
                    QuestionType = "Single",
                    Options = new List<string> { "Berlin", "Madrid", "Paris", "Rome" },
                    CorrectAnswers = new List<string> { "2" }
                },
                new Question
                {
                    Id = -3,
                    Text = "Which element has the chemical symbol O?",
                    QuestionType = "Single",
                    Options = new List<string> { "Gold", "Oxygen", "Iron", "Carbon" },
                    CorrectAnswers = new List<string> { "1" }
                },
                new Question
                {
                    Id = -4,
                    Text = "How many continents are on Earth?",
                    QuestionType = "Single",
                    Options = new List<string> { "5", "6", "7", "8" },
                    CorrectAnswers = new List<string> { "2" }
                },
                new Question
                {
                    Id = -5,
                    Text = "Spell the word 'quiz' backward.",
                    QuestionType = "Text",
                    CorrectAnswers = new List<string> { "ziuq" }
                },
                new Question
                {
                    Id = -6,
                    Text = "Enter the chemical formula for water.",
                    QuestionType = "Text",
                    CorrectAnswers = new List<string> { "h2o" }
                },
                new Question
                {
                    Id = -7,
                    Text = "Select prime numbers from below options:",
                    QuestionType = "Multiple",
                    Options = new List<string> { "2", "3", "4", "7", "10" },
                    CorrectAnswers = new List<string> { "0", "1", "3" }
                },
                new Question
                {
                    Id = -8,
                    Text = "Which of the following are programming languages?",
                    QuestionType = "Multiple",
                    Options = new List<string> { "Python", "Spanish", "Go", "German" },
                    CorrectAnswers = new List<string> { "0", "2" }
                }
            );

            // Seed QuizSubmissions
            modelBuilder.Entity<QuizSubmission>().HasData(
                new QuizSubmission
                {
                    Id = -1,
                    Email = "user1@example.com",
                    AnswersJson = "{\"1\": [\"2\"], \"2\": [\"0\", \"2\"], \"3\": [\"ziuq\"]}",
                    Score = 300,
                    SubmittedAt = DateTime.UtcNow.AddMinutes(-30)
                },
                new QuizSubmission
                {
                    Id = -2,
                    Email = "user2@example.com",
                    AnswersJson = "{\"1\": [\"2\"], \"2\": [\"2\"], \"3\": [\"quiz\"]}",
                    Score = 200,
                    SubmittedAt = DateTime.UtcNow.AddMinutes(-60)
                },
                new QuizSubmission
                {
                    Id = -3,
                    Email = "user3@example.com",
                    AnswersJson = "{\"1\": [\"1\"], \"2\": [\"0\"], \"3\": [\"ziuq\"]}",
                    Score = 150,
                    SubmittedAt = DateTime.UtcNow.AddMinutes(-120)
                }
            );
        }
    }
}
