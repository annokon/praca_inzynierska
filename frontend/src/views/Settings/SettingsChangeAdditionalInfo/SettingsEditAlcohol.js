import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditAlcohol() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [alcoholOptions, setAlcoholOptions] = useState([]);
    const [selectedAlcoholId, setSelectedAlcoholId] = useState("");
    const [status, setStatus] = useState("");
    const [initialized, setInitialized] = useState(false);

    const currentAlcoholName =
        typeof user?.alcohol === "string"
            ? user.alcohol
            : user?.alcohol?.name || "";

    const initialAlcoholId = useMemo(() => {
        if (!user || alcoholOptions.length === 0) return "";

        if (user?.alcohol?.id != null) {
            return String(user.alcohol.id);
        }

        const alcoholName =
            typeof user?.alcohol === "string"
                ? user.alcohol
                : user?.alcohol?.name;

        if (!alcoholName) return "";

        const matched = alcoholOptions.find(
            (option) => option.name.trim().toLowerCase() === alcoholName.trim().toLowerCase()
        );

        return matched ? String(matched.id) : "";
    }, [user, alcoholOptions]);

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
            } catch (err) {
                console.error("Błąd ładowania stosunku do alkoholu:", err);
                setStatus("Nie udało się pobrać listy opcji.");
            }
        };

        fetchOptions();
    }, []);

    useEffect(() => {
        if (!user || alcoholOptions.length === 0 || initialized) return;

        if (user?.alcohol?.id) {
            setSelectedAlcoholId(String(user.alcohol.id));
            setInitialized(true);
            return;
        }

        const alcoholName =
            typeof user?.alcohol === "string"
                ? user.alcohol
                : user?.alcohol?.name;

        if (alcoholName) {
            const matchedOption = alcoholOptions.find(
                (option) => option.name.trim().toLowerCase() === alcoholName.trim().toLowerCase()
            );

            if (matchedOption) {
                setSelectedAlcoholId(String(matchedOption.id));
                setInitialized(true);
                return;
            }
        }

        if (alcoholOptions.length > 0) {
            setSelectedAlcoholId(String(alcoholOptions[0].id));
        }

        setInitialized(true);
    }, [user, alcoholOptions, initialized]);

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

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/me", {
                method: "PATCH",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    alcoholId: selectedAlcoholId ? Number(selectedAlcoholId) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd zapisu stosunku do alkoholu:", res.status, data);
                setStatus(data.message || "Nie udało się zmienić stosunku do alkoholu.");
                return;
            }

            await refreshUser();
            navigate(-1);
        } catch (err) {
            console.error(err);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    const handleRemove = async () => {
        setStatus("");

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/me", {
                method: "PATCH",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    alcoholId: -1
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd usuwania stosunku do alkoholu:", res.status, data);
                setStatus(data.message || "Nie udało się usunąć stosunku do alkoholu.");
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
                                value={selectedAlcoholId}
                                className="form-input"
                                onChange={(e) => setSelectedAlcoholId(e.target.value)}
                            >
                                {alcoholOptions.map((a) => (
                                    <option key={a.id} value={String(a.id)}>
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