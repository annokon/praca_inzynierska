import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditBirthDate() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [status, setStatus] = useState("");

    const currentBirthDate = user?.birthDate || "";

    const formatBirthDate = (value, mode = "display") => {
        if (!value) return "";

        const onlyDate =
            typeof value === "string" && value.length >= 10
                ? value.slice(0, 10)
                : "";

        let normalized = onlyDate;

        if (!/^\d{4}-\d{2}-\d{2}$/.test(normalized)) {
            const date = new Date(value);
            if (Number.isNaN(date.getTime())) return "";

            const year = date.getFullYear();
            const month = String(date.getMonth() + 1).padStart(2, "0");
            const day = String(date.getDate()).padStart(2, "0");

            normalized = `${year}-${month}-${day}`;
        }

        if (mode === "input") {
            return normalized;
        }

        const [year, month, day] = normalized.split("-");
        return `${day}.${month}.${year}`;
    };

    const initialBirthDate = formatBirthDate(currentBirthDate, "input");
    const [newBirthDate, setNewBirthDate] = useState(initialBirthDate);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        if (!newBirthDate) {
            setStatus("Wybierz nową datę urodzenia.");
            return;
        }

        // if (newBirthDate === initialBirthDate) {
        //     setStatus("Nowa data urodzenia jest taka sama jak obecna.");
        //     return;
        // }

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/birth-date", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    birthDate: newBirthDate
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zmienić daty urodzenia.");
                return;
            }

            if (data) {
                setUser(data);
            } else {
                setUser((prev) => (prev ? { ...prev, birthDate: newBirthDate } : prev));
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
                    <p className="settings-subtitle">Edytuj datę urodzenia</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecna data urodzenia</label>
                            <div className="settings-value">
                                {loading
                                    ? "Ładowanie..."
                                    : formatBirthDate(currentBirthDate, "display") || "Brak danych"}
                            </div>
                        </div>

                        <div className="settings-field">
                            <label className="settings-label" htmlFor="birthDate">
                                Nowa data urodzenia
                            </label>
                            <input
                                id="birthDate"
                                name="birthDate"
                                type="date"
                                className="settings-input"
                                value={newBirthDate}
                                onChange={(e) => setNewBirthDate(e.target.value)}
                                required
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