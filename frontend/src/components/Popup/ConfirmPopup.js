import "./popup.css";

export default function ConfirmPopup({
                                         open,
                                         title,
                                         confirmLabel = "Usuń",
                                         cancelLabel = "Anuluj",
                                         onConfirm,
                                         onClose,
                                     }) {
    if (!open) return null;

    const handleOverlayClick = (e) => {
        if (e.target === e.currentTarget) onClose();
    };

    return (
        <div className="popup-overlay" onClick={handleOverlayClick}>
            <div
                className="popup"
                role="dialog"
                aria-modal="true"
                aria-label={title}
                onClick={(e) => e.stopPropagation()}
            >
                <h2 className="popup__title">{title}</h2>

                <div className="popup__actions">
                    <button
                        type="button"
                        className="btn btn--outline popup__button"
                        onClick={onClose}
                    >
                        {cancelLabel}
                    </button>

                    <button
                        type="button"
                        className="btn btn--primary popup__button"
                        onClick={onConfirm}
                    >
                        {confirmLabel}
                    </button>
                </div>
            </div>
        </div>
    );
}
