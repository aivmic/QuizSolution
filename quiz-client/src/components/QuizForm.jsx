import React, { useEffect, useState } from "react";
import { fetchQuestions } from "../api/quizApi";

const QuizForm = ({ onSubmit }) => {
    const [questions, setQuestions] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [currentStep, setCurrentStep] = useState(0);
    const [answers, setAnswers] = useState({});
    const [email, setEmail] = useState("");

    useEffect(() => {
        fetchQuestions()
            .then((response) => setQuestions(response.data || []))
            .catch((err) => setError(err.message))
            .finally(() => setLoading(false));
    }, []);

    const handleAnswerChange = (questionId, value, isMultiple = false) => {
        setAnswers((prev) => {
            const currentAnswers = prev[questionId] || [];
            if (isMultiple) {
                const updated = currentAnswers.includes(value)
                    ? currentAnswers.filter((v) => v !== value)
                    : [...currentAnswers, value];
                return { ...prev, [questionId]: updated };
            }
            return { ...prev, [questionId]: [value] };
        });
    };

    const handleNext = () => {
        if (currentStep < questions.length - 1) {
            setCurrentStep((prev) => prev + 1);
        }
    };

    const handlePrevious = () => {
        if (currentStep > 0) {
            setCurrentStep((prev) => prev - 1);
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (!email) {
            alert("Email is required");
            return;
        }
        onSubmit({ email, answers });
    };

    if (loading) return <p className="text-center text-gray-500">Loading questions...</p>;
    if (error) return <p className="text-center text-red-500">{error}</p>;
    if (questions.length === 0) return <p className="text-center text-gray-500">No questions available.</p>;

    const currentQuestion = questions[currentStep];

    return (
        <form className="max-w-2xl mx-auto p-6 bg-white rounded shadow-md" onSubmit={handleSubmit}>
            <div className="mb-4">
                <label className="block text-lg font-bold mb-2 text-primary-dark">Email:</label>
                <input
                    type="email"
                    className="w-full p-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-primary"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                />
            </div>
            {currentQuestion && (
                <div className="mb-6">
                    <h2 className="text-xl font-bold mb-4 text-primary-dark">
                        Q{currentStep + 1}: {currentQuestion.text}
                    </h2>
                    {currentQuestion.questionType === "Single" &&
                        currentQuestion.options.map((opt, idx) => (
                            <label key={idx} className="block mb-2">
                                <input
                                    type="radio"
                                    name={`question-${currentQuestion.id}`}
                                    value={idx}
                                    onChange={() => handleAnswerChange(currentQuestion.id, `${idx}`)}
                                    checked={answers[currentQuestion.id]?.[0] === `${idx}`}
                                    className="mr-2"
                                />
                                {opt}
                            </label>
                        ))}
                    {currentQuestion.questionType === "Multiple" &&
                        currentQuestion.options.map((opt, idx) => (
                            <label key={idx} className="block mb-2">
                                <input
                                    type="checkbox"
                                    value={idx}
                                    onChange={(e) => handleAnswerChange(currentQuestion.id, `${idx}`, true)}
                                    checked={answers[currentQuestion.id]?.includes(`${idx}`)}
                                    className="mr-2"
                                />
                                {opt}
                            </label>
                        ))}
                    {currentQuestion.questionType === "Text" && (
                        <input
                            type="text"
                            className="w-full p-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-primary"
                            onChange={(e) => handleAnswerChange(currentQuestion.id, e.target.value)}
                            value={answers[currentQuestion.id]?.[0] || ""}
                        />
                    )}
                </div>
            )}
            <div className="flex justify-between">
                <button
                    type="button"
                    onClick={handlePrevious}
                    disabled={currentStep === 0}
                    className="px-4 py-2 bg-gray-200 rounded hover:bg-gray-300"
                >
                    Previous
                </button>
                {currentStep === questions.length - 1 ? (
                    <button type="submit" className="px-4 py-2 bg-primary text-white rounded hover:bg-primary-dark">
                        Submit
                    </button>
                ) : (
                    <button
                        type="button"
                        onClick={handleNext}
                        className="px-4 py-2 bg-primary text-white rounded hover:bg-primary-dark"
                    >
                        Next
                    </button>
                )}
            </div>
        </form>
    );
};

export default QuizForm;
