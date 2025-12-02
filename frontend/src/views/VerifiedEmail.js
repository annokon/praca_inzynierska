import "../login/login.css";

export default function VerifiedEmail() {

    function handleContinue() {
        window.location.href = "/additional-info";
    }

    return (
        <div className="login-page">
            <div className="card">
                <h1 className="text-center">
                    Weryfikacja maila<br />przeszła pomyślnie
                </h1>

                <div className="text-center">
                    <button
                        type="button"
                        className="button"
                        onClick={handleContinue}
                    >
                        Kontynuuj rejestrację
                    </button>
                </div>
            </div>
        </div>
    );
}
