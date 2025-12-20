import {useState} from "react";
import "../../css/login_register.css";

export default function Register() {
    const [status, setStatus] = useState("");

    async function handleRegister(e) {
        e.preventDefault();
        setStatus("");

        const form = e.target;

        const username = form.username.value.trim();
        const displayName = form.displayName.value.trim();
        const email = form.email.value.trim();
        const birthDate = form.birthDate.value;
        const password = form.password.value;
        const confirmPassword = form.confirmPassword.value;
        const acceptTerms = form.terms.checked;

        if (password.length < 8) {
            setStatus("Hasło musi zawierać co najmniej 8 znaków.");
            return;
        }

        if (password !== confirmPassword) {
            setStatus("Hasła muszą być takie same.");
            return;
        }

        if (!acceptTerms) {
            setStatus("Musisz zaakceptować regulamin i potwierdzić wiek.");
            return;
        }

        try {
            setStatus("Tworzenie konta");

            const res = await fetch("http://localhost:5292/api/users/register", {
                method: "POST",
                credentials: "include",
                headers: {"Content-Type": "application/json"},
                body: JSON.stringify({
                    username,
                    displayName,
                    email,
                    birthDate,
                    password
                })
            });

            const data = await res.json();

            if (!res.ok) {
                setStatus(data.message || "Nie udało się utworzyć konta.");
                return;
            }

            localStorage.setItem("verifyEmail", email);

            await fetch("http://localhost:5292/api/email/send", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email })
            });

            window.location.href = "/verify-email";
        } catch (err) {
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    }

    return (
        <div className="auth">
            <div className="auth__page">
                <div className="auth__card">
                    <h1 className="auth__title auth__title--center">Zarejestruj się</h1>

                <form id="registerForm" onSubmit={handleRegister}>
                    <div className="form-field">
                        <label className="form-label" htmlFor="username">
                            Nazwa użytkownika
                        </label>
                        <input
                            id="username"
                            name="username"
                            type="text"
                            className="form-input"
                            required
                        />
                    </div>

                    <div className="form-field">
                        <label className="form-label" htmlFor="displayName">
                            Wyświetlana nazwa
                        </label>
                        <input
                            id="displayName"
                            name="displayName"
                            type="text"
                            className="form-input"
                            required
                        />
                    </div>

                    <div className="form-field">
                        <label className="form-label" htmlFor="email">
                            Email
                        </label>
                        <input
                            id="email"
                            name="email"
                            type="email"
                            className="form-input"
                            required
                        />
                    </div>

                    <div className="form-field">
                        <label className="form-label" htmlFor="birthDate">
                            Data urodzenia
                        </label>
                        <input
                            id="birthDate"
                            name="birthDate"
                            type="date"
                            className="form-input"
                            required
                        />
                    </div>

                    <div className="form-field">
                        <label className="form-label" htmlFor="password">
                            Hasło
                        </label>
                        <input
                            id="password"
                            name="password"
                            type="password"
                            className="form-input"
                            required
                        />
                        <div className="helper-text">
                            Hasło musi składać się z co najmniej 8 znaków.
                        </div>
                    </div>

                    <div className="form-field">
                        <label className="form-label" htmlFor="confirmPassword">
                            Powtórz hasło
                        </label>
                        <input
                            id="confirmPassword"
                            name="confirmPassword"
                            type="password"
                            className="form-input"
                            required
                        />
                    </div>

                    <div className="form-field checkbox-field">
                        <label className="checkbox-label" htmlFor="terms">
                        <input
                            id="terms"
                            name="terms"
                            type="checkbox"
                            required
                        />
                        <span>
                            Akceptuję regulamin aplikacji, zasady użytkowania i
                            potwierdzam, że mam ukończone 16 lat.
                        </span>
                    </label>
                    </div>

                    <div className="form-footer">
                    <button type="submit" className="btn btn--primary">
                        Stwórz konto
                    </button>

                    <button
                        type="button"
                        className="btn btn--secondary"
                        onClick={() => (window.location.href = "/login")}
                    >
                        Posiadasz już konto? Zaloguj się
                    </button>

                    <div id="status" className="form-field">
                        {status}
                    </div>
                    </div>
                </form>
            </div>
        </div>
        </div>
    );
}
