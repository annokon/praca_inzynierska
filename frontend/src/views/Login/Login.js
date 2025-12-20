import React, { useState } from "react";
import "../../css/login_register.css";
import useAuth from "../../hooks/useAuth";

export default function Login() {
    const [status, setStatus] = useState("");
    const { setUser } = useAuth();

    const handleLogin = async (e) => {
        e.preventDefault();

        const email = e.target.email.value;
        const password = e.target.password.value;

        setStatus("Logowanie...");

        const res = await fetch("http://localhost:5292/api/users/login", {
            method: "POST",
            credentials: "include",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password })
        });

        if (!res.ok) {
            setStatus("Nieprawidłowy email lub hasło.");
            return;
        }

        await new Promise(r => setTimeout(r, 50));

        const me = await fetch("http://localhost:5292/api/users/me", {
            credentials: "include"
        });

        if (!me.ok) {
            setStatus("Wystąpił błąd: nie udało się pobrać danych użytkownika.");
            return;
        }

        //TODO if email verified
        const userData = await me.json();

        const emailVerified = userData.emailVerified ?? userData.isEmailVerified ?? userData.isVerified;

        if (!emailVerified) {
            setUser(userData);
            setStatus("Musisz zweryfikować email.");
            window.location.href = "/verify-email";
            return;
        }

        setUser(userData);

        setStatus("Zalogowano pomyślnie!");
        setTimeout(() => {
            window.location.href = "/";
        }, 700);
    };

    return (
        <div className="auth">
            <div className="auth__page">
                <div className="auth__card">
                    <h1 className="auth__title auth__title--center">Logowanie</h1>
                    <p className="auth__subtitle auth__subtitle--center">
                        Wpisz email i hasło, aby się zalogować.
                    </p>

                <form id="loginForm" onSubmit={handleLogin}>
                    <div className="form-field">
                        <label className="form-label" htmlFor="email">
                            Email
                        </label>
                        <input id="email" name="email" type="email" className="form-input" required />
                    </div>

                    <div className="form-field">
                        <label className="form-label" htmlFor="password">
                            Hasło
                        </label>
                        <input id="password" name="password" type="password" className="form-input" required />
                        <button
                            type="button"
                            className="auth__link-forgot"
                            onClick={() => (window.location.href = "/forgot-password")}
                        >
                            Nie pamiętasz hasła?
                        </button>
                    </div>

                    <div className="form-footer">
                    <button type="submit" className="btn btn--primary">Zaloguj się</button>

                    <button
                        type="button"
                        className="btn btn--secondary"
                        onClick={() => (window.location.href = "/register")}
                    >
                        Nie masz konta? Stwórz nowe teraz
                    </button>

                    <div id="status" className="form-status">{status}</div>
                    </div>
                </form>
            </div>
        </div>
        </div>
    );
}
