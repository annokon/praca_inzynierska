import { useState } from "react";
import "./login/login.css";

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

            const res = await fetch("http://localhost:5292/api/auth/register", {
                method: "POST",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    username,
                    displayName,
                    email,
                    birthDate,
                    password
                })
            });

            if (!res.ok) {
                setStatus("Nie udało się utworzyć konta.");
                return;
            }

            window.location.href = "/verify-email";
        } catch (err) {
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    }

    return (
        <div className="card">
            <h1>Zarejestruj się</h1>

            <form id="registerForm" onSubmit={handleRegister}>
                <div className="field">
                    <label htmlFor="username">Nazwa użytkownika</label>
                    <input
                        id="username"
                        name="username"
                        type="text"
                        required
                    />
                </div>

                <div className="field">
                    <label htmlFor="displayName">Wyświetlana nazwa</label>
                    <input
                        id="displayName"
                        name="displayName"
                        type="text"
                        required
                    />
                </div>

                <div className="field">
                    <label htmlFor="email">Email</label>
                    <input
                        id="email"
                        name="email"
                        type="email"
                        required
                    />
                </div>

                <div className="field">
                    <label htmlFor="birthDate">Data urodzenia</label>
                    <input
                        id="birthDate"
                        name="birthDate"
                        type="date"
                        required
                    />
                </div>

                <div className="field">
                    <label htmlFor="password">Hasło</label>
                    <input
                        id="password"
                        name="password"
                        type="password"
                        required
                    />
                    <small>Hasło musi składać się z co najmniej 8 znaków.</small>
                </div>

                <div className="field">
                    <label htmlFor="confirmPassword">Powtórz hasło</label>
                    <input
                        id="confirmPassword"
                        name="confirmPassword"
                        type="password"
                        required
                    />
                </div>

                <div className="field checkbox-field">
                    <input
                        id="terms"
                        name="terms"
                        type="checkbox"
                        required
                    />
                    <label htmlFor="terms">
                        Akceptuję regulamin aplikacji, zasady użytkowania i
                        potwierdzam, że mam ukończone 16 lat.
                    </label>
                </div>

                <button type="submit" className="button">
                    Stwórz konto
                </button>

                <button
                    type="button"
                    className="button secondary"
                    onClick={() => (window.location.href = "/login")}
                >
                    Posiadasz już konto? Zaloguj się
                </button>

                <div id="status" className="status">
                    {status}
                </div>
            </form>
        </div>
    );
}
