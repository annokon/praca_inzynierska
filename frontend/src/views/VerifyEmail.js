import {useEffect, useState} from "react";
import "../css/login_register.css";

export default function VerifyEmail() {
    const [code, setCode] = useState("");
    const [status, setStatus] = useState("");
    const [email, setEmail] = useState("");

    useEffect(() => {
        const storedEmail = localStorage.getItem("verifyEmail");
        if (!storedEmail) {
            window.location.href = "/register";
            return;
        }
        setEmail(storedEmail);
    }, []);

    async function handleVerify(e) {
        e.preventDefault();
        setStatus("");

        const email = localStorage.getItem("verifyEmail");
        if (!email) {
            setStatus("Brak emaila do weryfikacji.");
            return;
        }

        try {
            const res = await fetch("http://localhost:5292/api/email/verify", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    email,
                    code
                })
            });

            if (!res.ok) {
                const msg = await res.text();
                setStatus(msg);
                return;
            }

            localStorage.removeItem("verifyEmail");
            window.location.href = "/verified-email";
        } catch {
            setStatus("Błąd połączenia z serwerem.");
        }
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
                            maxLength={6}
                            className="form-input"
                            value={code}
                            onChange={(e) => setCode(e.target.value.replace(/\D/g, ""))}
                            required
                        />
                    </div>

                    <button type="submit" className="btn btn--primary" disabled={code.length !== 6}>
                        Potwierdź
                    </button>

                    <div className="form-status">
                        {status}
                    </div>

                    <button
                        type="button"
                        className="btn btn-secondary"
                        onClick={async () => {
                            const email = localStorage.getItem("verifyEmail");
                            if (!email) return;

                            await fetch("http://localhost:5292/api/email/send", {
                                method: "POST",
                                headers: { "Content-Type": "application/json" },
                                body: JSON.stringify({ email })
                            });

                            setStatus("Nowy kod został wysłany.");
                        }}
                    >
                        Wyślij kod jeszcze raz
                    </button>
                </form>
            </div>
        </div>
        </div>
    );
}
