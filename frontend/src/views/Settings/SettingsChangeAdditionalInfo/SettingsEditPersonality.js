import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";
import "../../../css/login_register.css";

export default function SettingsEditPersonality() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [personalityOptions, setPersonalityOptions] = useState([]);
    const [selectedPersonalityId, setSelectedPersonalityId] = useState("");
    const [status, setStatus] = useState("");
    const [initialized, setInitialized] = useState(false);

    const currentPersonalityName =
        typeof user?.personality === "string"
            ? user.personality
            : user?.personality?.name || "";

    const initialPersonalityId = useMemo(() => {
        if (!user || personalityOptions.length === 0) return "";

        if (user?.personality?.id != null) {
            return String(user.personality.id);
        }

        const personalityName =
            typeof user?.personality === "string"
                ? user.personality
                : user?.personality?.name;

        if (!personalityName) return "";

        const matched = personalityOptions.find(
            (option) => option.name.trim().toLowerCase() === personalityName.trim().toLowerCase()
        );

        return matched ? String(matched.id) : "";
    }, [user, personalityOptions]);

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
                setPersonalityOptions(data.personalities || []);
            } catch (err) {
                console.error("Błąd ładowania typów osobowości:", err);
                setStatus("Nie udało się pobrać listy typów osobowości.");
            }
        };

        fetchOptions();
    }, []);

    useEffect(() => {
        if (!user || personalityOptions.length === 0 || initialized) return;

        if (user?.personality?.id) {
            setSelectedPersonalityId(String(user.personality.id));
            setInitialized(true);
            return;
        }

        const personalityName =
            typeof user?.personality === "string"
                ? user.personality
                : user?.personality?.name;

        if (personalityName) {
            const matchedOption = personalityOptions.find(
                (option) => option.name.trim().toLowerCase() === personalityName.trim().toLowerCase()
            );

            if (matchedOption) {
                setSelectedPersonalityId(String(matchedOption.id));
                setInitialized(true);
                return;
            }
        }

        if (personalityOptions.length > 0) {
            setSelectedPersonalityId(String(personalityOptions[0].id));
        }

        setInitialized(true);
    }, [user, personalityOptions, initialized]);

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
                    personalityId: selectedPersonalityId ? Number(selectedPersonalityId) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd zapisu osobowości:", res.status, data);
                setStatus(data.message || "Nie udało się zmienić osobowości.");
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
                    personalityId: -1
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd usuwania osobowości:", res.status, data);
                setStatus(data.message || "Nie udało się usunąć osobowości.");
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
                    <p className="settings-subtitle">Edytuj osobowość</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecna osobowość</label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentPersonalityName || "Brak danych"}
                            </div>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="personalityType">
                                Nowa osobowość
                            </label>
                            <select
                                id="personalityType"
                                value={selectedPersonalityId}
                                className="form-input"
                                onChange={(e) => setSelectedPersonalityId(e.target.value)}
                            >
                                {personalityOptions.map((p) => (
                                    <option key={p.id} value={String(p.id)}>
                                        {p.name}
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
                            disabled={loading}
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