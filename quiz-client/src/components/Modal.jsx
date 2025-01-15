import React from "react";

const Modal = ({ isOpen, title, message, onClose }) => {
    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
            <div className="bg-white p-6 rounded shadow-lg max-w-md w-full">
                <h2 className="text-xl font-bold text-primary-dark mb-4">{title}</h2>
                <p className="text-gray-700">{message}</p>
                <button
                    onClick={onClose}
                    className="mt-4 px-4 py-2 bg-primary text-white rounded hover:bg-primary-dark"
                >
                    Close
                </button>
            </div>
        </div>
    );
};

export default Modal;
