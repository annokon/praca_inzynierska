import { useState } from "react";
import "./login/login.css";

export default function Login() {
    const [status, setStatus] = useState("");

    async function handleLogin(e) {
        e.preventDefault();

        const email = e.target.email.value;
        const password = e.target.password.value;

        const res = await fetch("http://localhost:5292/api/auth/login", {
            method: "POST",
            credentials: "include",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password })
        });

        if (!res.ok) {
            setStatus("Nieprawidłowy email lub hasło.");
            return;
        }

        setStatus("Zalogowano pomyślnie");
        setTimeout(() => (window.location.href = "/"), 1000);
    }

    return (
        <div className="card">
            <h1>Logowanie</h1>

            <form onSubmit={handleLogin}>
                <div className="field">
                    <label>Email</label>
                    <input name="email" type="email" required />
                </div>

                <div className="field">
                    <label>Hasło</label>
                    <input name="password" type="password" required />
                </div>

                <button className="button">Zaloguj się</button>

                <div className="status">{status}</div>
            </form>
        </div>
    );
}
