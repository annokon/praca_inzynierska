import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditSmoking() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [smokingOptions, setSmokingOptions] = useState([]);
    const [smokingAttitude, setSmokingAttitude] = useState("");
    const [status, setStatus] = useState("");

    const currentSmokingId =
        user?.smokingPreferenceId ??
        user?.smokingPreference?.id ??
        "";

    const currentSmokingName =
        user?.smokingPreference?.name ??
        user?.smokingPreferenceName ??
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
                setSmokingOptions(data.smoking || []);
                setSmokingAttitude(currentSmokingId ? String(currentSmokingId) : "");
            } catch (err) {
                console.error("Błąd ładowania stosunku do papierosów:", err);
                setStatus("Nie udało się pobrać listy opcji.");
            }
        };

        fetchOptions();
    }, [currentSmokingId]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        if (String(smokingAttitude) === String(currentSmokingId || "")) {
            setStatus("Nowy stosunek do papierosów jest taki sam jak obecny.");
            return;
        }

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/smoking", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    smokingPreferenceId: smokingAttitude ? Number(smokingAttitude) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zmienić stosunku do papierosów.");
                return;
            }

            const selectedOption = smokingOptions.find(
                (option) => String(option.id) === String(smokingAttitude)
            );

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            smokingPreferenceId: smokingAttitude ? Number(smokingAttitude) : null,
                            smokingPreference: selectedOption
                                ? { id: selectedOption.id, name: selectedOption.name }
                                : null,
                            smokingPreferenceName: selectedOption ? selectedOption.name : null
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

        if (!currentSmokingId) {
            setStatus("Nie masz ustawionego stosunku do papierosów.");
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/smoking", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    smokingPreferenceId: null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się usunąć stosunku do papierosów.");
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            smokingPreferenceId: null,
                            smokingPreference: null,
                            smokingPreferenceName: null
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
                    <p className="settings-subtitle">Edytuj stosunek do papierosów</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecny stosunek do papierosów</label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentSmokingName || "Brak danych"}
                            </div>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="smokingAttitude">
                                Nowy stosunek do papierosów
                            </label>
                            <select
                                id="smokingAttitude"
                                value={smokingAttitude}
                                className="form-input"
                                onChange={(e) => setSmokingAttitude(e.target.value)}
                            >
                                <option value="">Select</option>
                                {smokingOptions.map((s) => (
                                    <option key={s.id} value={s.id}>
                                        {s.name}
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