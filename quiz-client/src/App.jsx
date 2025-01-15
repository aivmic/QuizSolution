import React from "react";
import { BrowserRouter as Router, Routes, Route, Link, Navigate } from "react-router-dom";
import QuizPage from "./pages/QuizPage";
import HighScoresPage from "./pages/HighScoresPage";

const App = () => {
    return (
        <Router>
            <nav className="bg-primary-dark p-4">
                <div className="container mx-auto flex justify-between items-center">
                    <Link to="/" className="text-white text-2xl font-bold">
                        QuizApp
                    </Link>
                    <div className="flex space-x-6">
                        <Link
                            to="/quiz"
                            className="text-white hover:text-primary-light font-bold text-lg"
                        >
                            Quiz
                        </Link>
                        <Link
                            to="/highscores"
                            className="text-white hover:text-primary-light font-bold text-lg"
                        >
                            High Scores
                        </Link>
                    </div>
                </div>
            </nav>
            <Routes>
                {/* Redirect `/` to `/quiz` */}
                <Route path="/" element={<Navigate to="/quiz" replace />} />
                <Route path="/quiz" element={<QuizPage />} />
                <Route path="/highscores" element={<HighScoresPage />} />
            </Routes>
        </Router>
    );
};

export default App;
