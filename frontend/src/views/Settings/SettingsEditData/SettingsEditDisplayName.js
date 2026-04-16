import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditDisplayName() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [newDisplayName, setNewDisplayName] = useState("");
    const [status, setStatus] = useState("");

    const currentDisplayName = user?.displayName || "";

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        const trimmedDisplayName = newDisplayName.trim();

        if (!trimmedDisplayName) {
            setStatus("Wpisz nową wyświetlaną nazwę.");
            return;
        }

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/me", {
                method: "PATCH",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    displayName: trimmedDisplayName
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zmienić wyświetlanej nazwy.");
                return;
            }

            if (data) {
                setUser(data);
            } else {
                setUser((prev) => prev ? { ...prev, displayName: trimmedDisplayName } : prev);
            }

            navigate(-1);
        } catch (err) {
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    return (
        <main className="settings-page">
            <section className="settings-panel settings-panel--form" aria-label="Ustawienia">
                <header className="settings-header">
                    <h1 className="settings-title">Ustawienia</h1>
                    <p className="settings-subtitle">Edytuj wyświetlaną nazwę</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecna nazwa</label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentDisplayName || "Brak danych"}
                            </div>
                        </div>

                        <div className="settings-field">
                            <label htmlFor="displayName" className="settings-label">
                                Nowa nazwa
                            </label>
                            <input
                                id="displayName"
                                type="text"
                                className="settings-input"
                                value={newDisplayName}
                                onChange={(e) => setNewDisplayName(e.target.value)}
                                autoComplete="off"
                            />
                        </div>
                    </section>

                    <div className="settings-actions">
                        <button
                            type="button"
                            className="settings-btn settings-btn--ghost"
                            onClick={() => navigate(-1)}
                        >
                            Anuluj
                        </button>

                        <button
                            type="submit"
                            className="settings-btn settings-btn--primary"
                            disabled={loading}
                        >
                            Zatwierdź
                        </button>
                    </div>

                    {status ? <p className="settings-status">{status}</p> : null}
                </form>
            </section>
        </main>
    );
}