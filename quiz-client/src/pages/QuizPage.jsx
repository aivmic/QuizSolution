import React, { useState } from "react";
import QuizForm from "../components/QuizForm";
import { submitQuiz } from "../api/quizApi";
import Modal from "../components/Modal";

const QuizPage = () => {
    const [score, setScore] = useState(null);
    const [showModal, setShowModal] = useState(false);
    const [retakeQuiz, setRetakeQuiz] = useState(false);

    const handleQuizSubmit = async (data) => {
        const result = await submitQuiz(data);
        setScore(result.score);
        setShowModal(true);
        setRetakeQuiz(false);
    };

    const handleRetakeQuiz = () => {
        setScore(null); // Clear the score
        setShowModal(false); // Close the modal
        setRetakeQuiz(true); // Reset the quiz form
    };

    const closeModal = () => setShowModal(false);

    return (
        <div className="container mx-auto p-6 bg-primary-light min-h-screen">
            <h1 className="text-3xl font-bold text-primary-dark mb-6 text-center">
                Solve the Quiz
            </h1>
            {!score ? (
                <QuizForm onSubmit={handleQuizSubmit} resetForm={retakeQuiz} />
            ) : (
                <div className="flex flex-col items-center mt-8">
                    <div className="bg-white p-6 rounded shadow text-center">
                        <h2 className="text-2xl font-bold text-primary-dark mb-4">
                            Your Score: {score}
                        </h2>
                        <button
                            onClick={handleRetakeQuiz}
                            className="px-4 py-2 bg-primary text-white rounded hover:bg-primary-dark mt-4"
                        >
                            Retake Quiz
                        </button>
                    </div>
                </div>
            )}

            {/* Modal */}
            <Modal
                isOpen={showModal}
                title="Quiz Submitted!"
                message="Your score has been calculated. Check it above!"
                onClose={closeModal}
            />
        </div>
    );
};

export default QuizPage;
