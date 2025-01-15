import React, { useEffect, useState } from "react";
import { fetchHighScores } from "../api/quizApi";

const HighScores = () => {
    const [scores, setScores] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchHighScores()
            .then((response) => setScores(response.data || []))
            .catch((err) => setError(err.message))
            .finally(() => setLoading(false));
    }, []);

    const getMedalClass = (position) => {
        if (position === 1) return "text-yellow-500 font-bold"; // Gold
        if (position === 2) return "text-gray-400 font-bold"; // Silver
        if (position === 3) return "text-orange-600 font-bold"; // Bronze
        return "";
    };

    if (loading) return <p className="text-center text-gray-500">Loading high scores...</p>;
    if (error) return <p className="text-center text-red-500">{error}</p>;
    if (scores.length === 0) return <p className="text-center text-gray-500">No high scores found.</p>;

    return (
        <div className="max-w-4xl mx-auto mt-8">
            <h1 className="text-3xl font-bold text-center mb-6">High Scores</h1>
            <table className="table-auto w-full border-collapse border border-gray-300">
                <thead>
                    <tr>
                        <th className="border border-gray-300 px-4 py-2">Position</th>
                        <th className="border border-gray-300 px-4 py-2">Email</th>
                        <th className="border border-gray-300 px-4 py-2">Score</th>
                        <th className="border border-gray-300 px-4 py-2">DateTime</th>
                    </tr>
                </thead>
                <tbody>
                    {scores.map((entry, index) => (
                        <tr key={index} className={`text-center ${getMedalClass(index + 1)}`}>
                            <td className="border border-gray-300 px-4 py-2">{entry.position}</td>
                            <td className="border border-gray-300 px-4 py-2">{entry.email}</td>
                            <td className="border border-gray-300 px-4 py-2">{entry.score}</td>
                            <td className="border border-gray-300 px-4 py-2">
                                {new Date(entry.submittedAt).toLocaleString()}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default HighScores;
