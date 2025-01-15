using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace QuizApp.Models
{
    public class QuizSubmission
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string AnswersJson { get; set; } = string.Empty; 

        [NotMapped]
        public Dictionary<int, List<string>> Answers
        {
            get => string.IsNullOrEmpty(AnswersJson)
                ? new Dictionary<int, List<string>>()
                : JsonSerializer.Deserialize<Dictionary<int, List<string>>>(AnswersJson);
            set => AnswersJson = JsonSerializer.Serialize(value);
        }
        public int Score { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}