import { useState } from "react";
import "../css/login_register.css";

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
        <div className="auth">
            <div className="auth__page">
                <div className="auth__card">
                <h1 className="auth__title" >Weryfikacja maila</h1>

                <p className="auth__subtitle auth__subtitle--center">
                    Na adres <strong>twojego adresu e-mail</strong> został
                    wysłany mail z kodem do weryfikacji.
                </p>

                <form onSubmit={handleVerify}>
                    <div className="form-group">
                        <label className="form-field" htmlFor="code">
                            Wprowadź kod
                        </label>
                        <input
                            id="code"
                            name="code"
                            type="text"
                            className="form-input"
                            value={code}
                            onChange={(e) => setCode(e.target.value)}
                            required
                        />
                    </div>

                    <button type="submit" className="btn btn-primary">
                        Potwierdź
                    </button>

                    <div className="form-status">
                        {status}
                    </div>
                </form>
            </div>
        </div>
        </div>
    );
}
