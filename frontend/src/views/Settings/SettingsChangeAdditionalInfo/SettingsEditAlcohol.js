import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditAlcohol() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [alcoholOptions, setAlcoholOptions] = useState([]);
    const [alcoholAttitude, setAlcoholAttitude] = useState("");
    const [status, setStatus] = useState("");

    const currentAlcoholId =
        user?.alcoholPreferenceId ??
        user?.alcoholPreference?.id ??
        "";

    const currentAlcoholName =
        user?.alcoholPreference?.name ??
        user?.alcoholPreferenceName ??
        "";

    useEffect(() => {
        const fetchOptions = async () => {
            try {
                const res = await fetch("http://localhost:5292/api/options", {
                    method: "GET",
                    headers: { "Content-Type": "application/json" },
                    credentials: "include"
                });

                if (!res.ok) {
                    throw new Error("Nie udało się pobrać opcji.");
                }

                const data = await res.json();
                setAlcoholOptions(data.alcohol || []);
                setAlcoholAttitude(currentAlcoholId ? String(currentAlcoholId) : "");
            } catch (err) {
                console.error("Błąd ładowania stosunku do alkoholu:", err);
                setStatus("Nie udało się pobrać listy opcji.");
            }
        };

        fetchOptions();
    }, [currentAlcoholId]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/alcohol", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    alcoholPreferenceId: alcoholAttitude ? Number(alcoholAttitude) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zmienić stosunku do alkoholu.");
                return;
            }

            const selectedOption = alcoholOptions.find(
                (option) => String(option.id) === String(alcoholAttitude)
            );

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            alcoholPreferenceId: alcoholAttitude ? Number(alcoholAttitude) : null,
                            alcoholPreference: selectedOption
                                ? { id: selectedOption.id, name: selectedOption.name }
                                : null,
                            alcoholPreferenceName: selectedOption ? selectedOption.name : null
                        }
                        : prev
                );
            }

            navigate(-1);
        } catch (err) {
            console.error(err);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    const handleRemove = async () => {
        setStatus("");

        if (!currentAlcoholId) {
            setStatus("Nie masz ustawionego stosunku do alkoholu.");
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/alcohol", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    alcoholPreferenceId: null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się usunąć stosunku do alkoholu.");
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            alcoholPreferenceId: null,
                            alcoholPreference: null,
                            alcoholPreferenceName: null
                        }
                        : prev
                );
            }

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
                    <p className="settings-subtitle">Edytuj stosunek do alkoholu</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecny stosunek do alkoholu</label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentAlcoholName || "Brak danych"}
                            </div>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="alcoholAttitude">
                                Nowy stosunek do alkoholu
                            </label>
                            <select
                                id="alcoholAttitude"
                                value={alcoholAttitude}
                                className="form-input"
                                onChange={(e) => setAlcoholAttitude(e.target.value)}
                            >
                                <option value="">Select</option>
                                {alcoholOptions.map((a) => (
                                    <option key={a.id} value={a.id}>
                                        {a.name}
                                    </option>
                                ))}
                            </select>
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
                            type="button"
                            className="settings-btn settings-btn--ghost"
                            onClick={handleRemove}
                            disabled={loading}
                        >
                            Usuń
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