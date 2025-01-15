// only for learning purposes
const BASE_URL = "http://localhost:5109/api/v1";

export async function fetchQuestions() {
    const response = await fetch(`${BASE_URL}/questions`);
    if (!response.ok) throw new Error((await response.json()).message || "Failed to fetch questions.");
    return response.json();
}

export async function submitQuiz(data) {
    const response = await fetch(`${BASE_URL}/submit`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
    });
    if (!response.ok) throw new Error((await response.json()).message || "Failed to submit quiz.");
    return response.json();
}

export async function fetchHighScores() {
    const response = await fetch(`${BASE_URL}/highscores`);
    if (!response.ok) throw new Error((await response.json()).message || "Failed to fetch high scores.");
    return response.json();
}
