import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";
import "../../../css/login_register.css";

export default function SettingsEditTravelExperience() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [travelExperienceOptions, setTravelExperienceOptions] = useState([]);
    const [selectedTravelExperienceId, setSelectedTravelExperienceId] = useState("");
    const [status, setStatus] = useState("");
    const [initialized, setInitialized] = useState(false);

    const currentTravelExperienceName =
        typeof user?.travelExperience === "string"
            ? user.travelExperience
            : user?.travelExperience?.name || "";

    const initialTravelExperienceId = useMemo(() => {
        if (!user || travelExperienceOptions.length === 0) return "";

        if (user?.travelExperience?.id != null) {
            return String(user.travelExperience.id);
        }

        const travelExperienceName =
            typeof user?.travelExperience === "string"
                ? user.travelExperience
                : user?.travelExperience?.name;

        if (!travelExperienceName) return "";

        const matched = travelExperienceOptions.find(
            (option) =>
                option.name.trim().toLowerCase() ===
                travelExperienceName.trim().toLowerCase()
        );

        return matched ? String(matched.id) : "";
    }, [user, travelExperienceOptions]);

    useEffect(() => {
        const fetchOptions = async () => {
            try {
                const res = await fetch("http://localhost:5292/api/options", {
                    method: "GET",
                    headers: { "Content-Type": "application/json" },
                    credentials: "include"
                });

                if (!res.ok) {
                    throw new Error("Nie udało się pobrać poziomów doświadczenia.");
                }

                const data = await res.json();
                setTravelExperienceOptions(data.travelExperience || []);
            } catch (err) {
                console.error("Błąd ładowania poziomów doświadczenia:", err);
                setStatus("Nie udało się pobrać listy poziomów doświadczenia.");
            }
        };

        fetchOptions();
    }, []);

    useEffect(() => {
        if (!user || travelExperienceOptions.length === 0 || initialized) return;

        if (user?.travelExperience?.id) {
            setSelectedTravelExperienceId(String(user.travelExperience.id));
            setInitialized(true);
            return;
        }

        const travelExperienceName =
            typeof user?.travelExperience === "string"
                ? user.travelExperience
                : user?.travelExperience?.name;

        if (travelExperienceName) {
            const matchedOption = travelExperienceOptions.find(
                (option) =>
                    option.name.trim().toLowerCase() ===
                    travelExperienceName.trim().toLowerCase()
            );

            if (matchedOption) {
                setSelectedTravelExperienceId(String(matchedOption.id));
                setInitialized(true);
                return;
            }
        }

        if (travelExperienceOptions.length > 0) {
            setSelectedTravelExperienceId(String(travelExperienceOptions[0].id));
        }

        setInitialized(true);
    }, [user, travelExperienceOptions, initialized]);

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
                    travelExperienceId: selectedTravelExperienceId
                        ? Number(selectedTravelExperienceId)
                        : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd zapisu poziomu doświadczenia:", res.status, data);
                setStatus(data.message || "Nie udało się zmienić poziomu doświadczenia.");
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
                    travelExperienceId: -1
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd usuwania poziomu doświadczenia:", res.status, data);
                setStatus(data.message || "Nie udało się usunąć poziomu doświadczenia.");
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
                    <p className="settings-subtitle">Edytuj poziom doświadczenia w podróżach</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">
                                Obecny poziom doświadczenia w podróżach
                            </label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentTravelExperienceName || "Brak danych"}
                            </div>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="travelExperience">
                                Nowy poziom doświadczenia w podróżach
                            </label>
                            <select
                                id="travelExperience"
                                value={selectedTravelExperienceId}
                                className="form-input"
                                onChange={(e) => setSelectedTravelExperienceId(e.target.value)}
                            >
                                {travelExperienceOptions.map((option) => (
                                    <option key={option.id} value={String(option.id)}>
                                        {option.name}
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