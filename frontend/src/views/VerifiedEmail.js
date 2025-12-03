import "../css/login_register.css";

export default function VerifiedEmail() {

    function handleContinue() {
        window.location.href = "/additional-info";
    }

    return (
        <div className="auth">
            <div className="auth__page">
                <div className="auth__card">
                    <h1 className="auth__title auth__title--center">
                    Weryfikacja maila<br />przeszła pomyślnie
                </h1>

                <div className="text-center">
                    <button
                        type="button"
                        className="btn btn--primary"
                        onClick={handleContinue}
                    >
                        Kontynuuj rejestrację
                    </button>
                </div>
            </div>
        </div>
        </div>
    );
}
