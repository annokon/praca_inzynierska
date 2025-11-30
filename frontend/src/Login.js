import React, { useState } from "react";
import "./login/login.css";
import useAuth from "./hooks/useAuth";

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

        const userData = await me.json();

        setUser(userData);

        setStatus("Zalogowano pomyślnie!");
        setTimeout(() => {
            window.location.href = "/";
        }, 700);
    };

    return (
        <div className="login-page">
            <div className="card">
                <h1>Logowanie</h1>
                <p>Wpisz email i hasło, aby się zalogować.</p>

                <form id="loginForm" onSubmit={handleLogin}>
                    <div className="field">
                        <label>Email</label>
                        <input id="email" name="email" type="email" required />
                    </div>

                    <div className="field">
                        <label>Hasło</label>
                        <input id="password" name="password" type="password" required />
                    </div>

                    <button type="submit" className="button">Zaloguj się</button>

                    <button
                        type="button"
                        className="button secondary"
                        onClick={() => (window.location.href = "/register")}
                    >
                        Nie masz konta? Stwórz nowe teraz
                    </button>

                    <div id="status" className="status">{status}</div>
                </form>
            </div>
        </div>
    );
}
