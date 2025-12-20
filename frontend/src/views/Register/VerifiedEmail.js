import "../../css/login_register.css";
import {useState} from "react";

export default function VerifiedEmail() {
    function handleContinue() {
        window.location.href = "/additional-info";
    }

    return (
        <div className="auth auth--verified-email">
            <div className="auth__page">
                <div className="auth__card">
                    <div className="verified-email__center">
                    <h1 className="auth__title auth__title--center">
                    Weryfikacja maila<br />przeszła pomyślnie
                </h1>

                    <div className="verified-email__icon" aria-hidden="true">
                        <svg viewBox="0 0 24 24" fill="none">
                            <path
                                d="M20 7L9 18L4 13"
                                stroke="currentColor"
                                strokeWidth="1.8"
                                strokeLinecap="round"
                                strokeLinejoin="round"
                            />
                        </svg>
                    </div>
                </div>

                        <div className="form-footer">
                        <button
                            type="button"
                            className="btn btn--primary"
                            onClick={handleContinue}
                        >
                            Kontynuuj rejestrację
                        </button>
                            <div className="form-status">
                        </div>
                    </div>
            </div>
        </div>
        </div>
    );
}
