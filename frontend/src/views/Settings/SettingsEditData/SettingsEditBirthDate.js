import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditBirthDate() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [newBirthDate, setNewBirthDate] = useState("");
    const [hideAge, setHideAge] = useState(false);
    const [status, setStatus] = useState("");
    const [initialized, setInitialized] = useState(false);

    const currentBirthDate = user?.birthDate || "";

    const currentHideAge =
        user?.hideAge ??
        user?.isAgeHidden ??
        user?.hideAgeOnProfile ??
        false;

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

    const initialBirthDate = useMemo(() => {
        return formatBirthDate(currentBirthDate, "input");
    }, [currentBirthDate]);

    useEffect(() => {
        if (initialized) return;
        if (loading) return;

        setNewBirthDate(initialBirthDate);
        setHideAge(Boolean(currentHideAge));
        setInitialized(true);
    }, [initialBirthDate, currentHideAge, initialized, loading]);

    const refreshUser = async () => {
        const meRes = await fetch("http://localhost:5292/api/users/me", {
            credentials: "include"
        });

        if (!meRes.ok) {
            throw new Error("Nie udało się odświeżyć danych użytkownika.");
        }

        const meData = await meRes.json();
        setUser(meData);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        if (!newBirthDate) {
            setStatus("Wybierz nową datę urodzenia.");
            return;
        }

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/me", {
                method: "PATCH",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    birthDate: newBirthDate,
                    hideAge: hideAge
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd zapisu daty urodzenia:", res.status, data);
                setStatus(data.message || "Nie udało się zmienić daty urodzenia.");
                return;
            }

            await refreshUser();
            navigate(-1);
        } catch (err) {
            console.error(err);
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

                        <div className="settings-field">
                            <label className="settings-label">Widoczność wieku</label>

                            <label className="settings-switch-row" htmlFor="hideAge">
                                <span className="settings-switch-text">
                                    Ukryj wiek na profilu
                                </span>

                                <input
                                    id="hideAge"
                                    name="hideAge"
                                    type="checkbox"
                                    className="settings-switch-input"
                                    checked={hideAge}
                                    onChange={(e) => setHideAge(e.target.checked)}
                                />

                                <span className="settings-switch" aria-hidden="true">
                                    <span className="settings-switch-thumb" />
                                </span>
                            </label>
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
                            disabled={loading || !initialized}
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