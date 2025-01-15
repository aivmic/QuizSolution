using Microsoft.AspNetCore.Mvc;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Services;
using QuizApp.Utils.Mappers;

namespace QuizApp.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class QuizController : ControllerBase
    {
        private readonly QuizDbContext _context;
        private readonly IScoringService _scoringService;
        private readonly ILogger<QuizController> _logger;

        public QuizController(QuizDbContext context, IScoringService scoringService, ILogger<QuizController> logger)
        {
            _context = context;
            _scoringService = scoringService;
            _logger = logger;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            try
            {
                var questions = await Task.Run(() =>
                    _context.Questions.Select(QuizMapper.ToDto).ToList()
                );

                if (questions == null || !questions.Any())
                {
                    _logger.LogWarning("No questions found.");
                    return NotFound(new { message = "No questions available." });
                }

                return Ok(new { data = questions });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching questions.");
                return StatusCode(500, new { message = "An error occurred while fetching questions." });
            }
        }

        [HttpGet("highscores")]
        public async Task<IActionResult> GetHighScores()
        {
            try
            {
                var highScores = await Task.Run(() =>
                    _context.QuizSubmissions
                        .OrderByDescending(s => s.Score)
                        .ThenBy(s => s.SubmittedAt)
                        .Take(10)
                        .ToList()
                );

                if (!highScores.Any())
                {
                    _logger.LogWarning("No high scores found.");
                    return NotFound(new { message = "No high scores found." });
                }

                var result = highScores.Select((submission, index) =>
                    QuizMapper.ToDto(submission, index + 1)).ToList();

                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching high scores.");
                return StatusCode(500, new { message = "An error occurred while fetching high scores." });
            }
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitQuiz([FromBody] QuizSubmission submission)
        {
            if (submission == null || string.IsNullOrWhiteSpace(submission.Email) || submission.Answers == null || !submission.Answers.Any())
            {
                return BadRequest(new { message = "Invalid submission data." });
            }

            try
            {
                int totalScore = 0;
                var questions = await Task.Run(() => _context.Questions.ToList());

                foreach (var answer in submission.Answers)
                {
                    var question = questions.FirstOrDefault(q => q.Id == answer.Key);
                    if (question == null)
                    {
                        _logger.LogWarning($"Question with ID {answer.Key} not found.");
                        continue;
                    }

                    totalScore += _scoringService.CalculateScore(question, answer.Value);
                }

                submission.Score = totalScore;
                submission.SubmittedAt = DateTime.UtcNow;

                await _context.QuizSubmissions.AddAsync(submission);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Quiz submitted successfully.", score = totalScore });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting quiz.");
                return StatusCode(500, new { message = "An error occurred while submitting the quiz." });
            }
        }
    }
}
