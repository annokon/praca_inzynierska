import { useState } from "react";
import "../login/login.css";

export default function VerifyEmail() {
    const [code, setCode] = useState("");
    const [status, setStatus] = useState("");

    function handleVerify(e) {
        e.preventDefault();

        if (!code.trim()) {
            setStatus("Wprowadź kod weryfikacyjny.");
            return;
        }
        setStatus("");

        window.location.href = "/verified-email";
    }

    return (
        <div className="login-page">
            <div className="card">
                <h1>Weryfikacja maila</h1>

                <p className="text-center">
                    Na adres <strong>twojego adresu e-mail</strong> został
                    wysłany mail z kodem do weryfikacji.
                </p>

                <form onSubmit={handleVerify}>
                    <div className="field">
                        <label htmlFor="code">Wprowadź kod</label>
                        <input
                            id="code"
                            name="code"
                            type="text"
                            value={code}
                            onChange={(e) => setCode(e.target.value)}
                            required
                        />
                    </div>

                    <button type="submit" className="button">
                        Potwierdź
                    </button>

                    <div className="status">
                        {status}
                    </div>
                </form>
            </div>
        </div>
    );
}
