using System.Collections.Generic;
namespace QuizApp.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string QuestionType { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new List<string>();
    public List<string> CorrectAnswers { get; set; } = new List<string>();
}