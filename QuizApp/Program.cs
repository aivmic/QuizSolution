using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<QuizDbContext>(options => options.UseInMemoryDatabase("QuizDb"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IScoringService, ScoringService>();
builder.Services.AddControllers();
builder.Services.AddCors(options => 
    options.AddPolicy("AllowAll", policy => 
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

// Do not configure Swagger services
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure the database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<QuizDbContext>();
    context.Database.EnsureCreated();
}

// Configure middleware
app.UseCors("AllowAll");

// Swagger is removed, so these lines are commented out
// app.UseSwagger();
// app.UseSwaggerUI();

app.MapControllers();
app.Run();