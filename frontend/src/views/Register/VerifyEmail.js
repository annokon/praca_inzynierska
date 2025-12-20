import { useEffect, useMemo, useRef, useState } from "react";
import "../../css/login_register.css";

export default function VerifyEmail() {
    const CODE_LEN = 6;

    const [digits, setDigits] = useState(Array(CODE_LEN).fill(""));
    const [status, setStatus] = useState("");
    const [email, setEmail] = useState("");

    const inputsRef = useRef([]);
    const code = useMemo(() => digits.join(""), [digits]);

    useEffect(() => {
        const storedEmail = localStorage.getItem("verifyEmail");

        setEmail(storedEmail);
    }, []);
    const focusIndex = (i) => inputsRef.current[i]?.focus();

    const handleDigitChange = (i, raw) => {
        const v = raw.replace(/\D/g, "");

        if (v.length > 1) {
            const next = [...digits];
            let idx = i;
            for (const ch of v) {
                if (idx >= CODE_LEN) break;
                next[idx] = ch;
                idx++;
            }
            setDigits(next);
            focusIndex(Math.min(idx, CODE_LEN - 1));
            return;
        }

        const next = [...digits];
        next[i] = v;
        setDigits(next);

        if (v && i < CODE_LEN - 1) focusIndex(i + 1);
    };

    const handleDigitKeyDown = (i, e) => {
        if (e.key === "Backspace") {
            if (!digits[i] && i > 0) {
                const next = [...digits];
                next[i - 1] = "";
                setDigits(next);
                focusIndex(i - 1);
                e.preventDefault();
            } else {
                const next = [...digits];
                next[i] = "";
                setDigits(next);
                e.preventDefault();
            }
        }

        if (e.key === "ArrowLeft" && i > 0) focusIndex(i - 1);
        if (e.key === "ArrowRight" && i < CODE_LEN - 1) focusIndex(i + 1);
    };

    const handleDigitsPaste = (e) => {
        const text = e.clipboardData.getData("text").replace(/\D/g, "");
        if (!text) return;

        const next = Array(CODE_LEN).fill("");
        for (let i = 0; i < CODE_LEN; i++) next[i] = text[i] ?? "";
        setDigits(next);

        const last = Math.min(text.length, CODE_LEN) - 1;
        focusIndex(Math.max(last, 0));
        e.preventDefault();
    };

    async function handleVerify(e) {
        e.preventDefault();
        setStatus("");

        const email = localStorage.getItem("verifyEmail");
        if (!email) {
            setStatus("Brak emaila do weryfikacji.");
            return;
        }
        if (code.length !== CODE_LEN) {
            setStatus("Wpisz pełny kod.");
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
                    <h1 className="auth__title auth__title--center" >Weryfikacja maila</h1>

                    <p className="auth__subtitle auth__subtitle--center">
                        Na adres <strong>twojego adresu e-mail</strong> został
                        wysłany mail z kodem do weryfikacji.
                    </p>

                    <form onSubmit={handleVerify} className="auth__form">
                        <div className="form-field">
                            <label className="form-label" htmlFor="code">Wprowadź kod</label>
                            <div className="otp-fields" onPaste={handleDigitsPaste}>
                                {digits.map((d, i) => (
                                    <input
                                        key={i}
                                        id={`code-${i}`}
                                        ref={(el) => {(inputsRef.current[i] = el)}}
                                        type="tel"
                                        inputMode="numeric"
                                        pattern="[0-9]"
                                        maxLength={1}
                                        className="otp-input"
                                        placeholder="•"
                                        value={d}
                                        onChange={(e) => handleDigitChange(i, e.target.value)}
                                        onKeyDown={(e) => handleDigitKeyDown(i, e)}
                                        aria-label={`Cyfra ${i + 1} kodu`}
                                        required
                                    />
                                ))}
                            </div>
                        </div>

                        <div className="form-status">{status}</div>

                        <div className="form-footer">
                            <button
                                type="submit"
                                className="btn btn--primary"
                                disabled={code.length !== 6}
                            >
                                Potwierdź
                            </button>

                        <button
                            type="button"
                            className="btn btn--secondary"
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
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
}