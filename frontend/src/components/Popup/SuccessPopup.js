import "./popup.css"
export default function SuccessPopup({ open, title, buttonLabel = "Okej", onClose }) {
    if (!open) return null;
    return (
        <div className="popup-overlay">
            <div className="popup">
                <h2 className="popup__title">{title}</h2>

                <button
                    type="button"
                    className="btn btn--primary popup__button"
                    onClick={onClose}
                >
                    {buttonLabel}
                </button>
            </div>
        </div>
    );
}