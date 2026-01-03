import React, { useEffect, useState } from "react";
import "./popup.css";
import StarsPicker from "./StarsPicker";

export default function ReviewPopup({
                                        open,
                                        targetName = "",
                                        onClose,
                                        onSave,
                                    }) {
    const [rating, setRating] = useState(0);
    const [text, setText] = useState("");

    useEffect(() => {
        if (open) {
            setRating(0);
            setText("");
        }
    }, [open]);

    if (!open) return null;

    const handleOverlayClick = (e) => {
        if (e.target === e.currentTarget) onClose();
    };

    const handleSave = () => {
        onSave({ rating, text });
    };

    return (
        <div className="popup-overlay" onClick={handleOverlayClick}>
            <div className="popup popup--review" role="dialog" aria-modal="true" onClick={(e) => e.stopPropagation()}>
                <h2 className="popup__title">Wystaw opinię o użytkowniku</h2>

                <div className="popup__divider" />

                <div className="review__subtitle">Oceń użytkownika</div>

                <StarsPicker value={rating} onChange={setRating} max={10} />

                <textarea
                    className="review__textarea"
                    value={text}
                    onChange={(e) => setText(e.target.value)}
                    placeholder="Napisz opinię..."
                    aria-label={targetName ? `Opinia o użytkowniku ${targetName}` : "Opinia"}
                />

                <div className="popup__actions">
                    <button type="button" className="btn btn--outline" onClick={onClose}>
                        Anuluj
                    </button>
                    <button type="button" className="btn btn--primary" onClick={handleSave}>
                        Zapisz
                    </button>
                </div>
            </div>
        </div>
    );
}
