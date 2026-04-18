import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";
import "../../../css/login_register.css";

function getUserTransportModeIds(user, allTransportModes) {
    if (!user) return [];

    if (Array.isArray(user.transport)) {
        return user.transport
            .map((modeName) => {
                if (typeof modeName !== "string") return null;

                const matched = allTransportModes.find(
                    (option) =>
                        option.name.trim().toLowerCase() === modeName.trim().toLowerCase()
                );

                return matched?.id ?? null;
            })
            .filter(Boolean);
    }

    if (Array.isArray(user.transportModes)) {
        return user.transportModes
            .map((mode) => {
                if (typeof mode === "number") return mode;

                if (typeof mode === "string") {
                    const matched = allTransportModes.find(
                        (option) =>
                            option.name.trim().toLowerCase() === mode.trim().toLowerCase()
                    );
                    return matched?.id ?? null;
                }

                if (typeof mode === "object" && mode !== null) {
                    if (mode.id != null) return mode.id;
                    if (mode.transportModeId != null) return mode.transportModeId;

                    if (mode.name) {
                        const matched = allTransportModes.find(
                            (option) =>
                                option.name.trim().toLowerCase() === mode.name.trim().toLowerCase()
                        );
                        return matched?.id ?? null;
                    }
                }

                return null;
            })
            .filter(Boolean);
    }

    if (Array.isArray(user.transportModeIds)) {
        return user.transportModeIds.filter(Boolean);
    }

    if (Array.isArray(user.userTransportModes)) {
        return user.userTransportModes
            .map((mode) => mode.transportModeId ?? mode.id)
            .filter(Boolean);
    }

    if (user.transportModeId != null) {
        return [user.transportModeId];
    }

    if (user.transportMode?.id != null) {
        return [user.transportMode.id];
    }

    return [];
}

export default function SettingsEditTransportMode() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [allTransportModes, setAllTransportModes] = useState([]);
    const [selectedTransportModes, setSelectedTransportModes] = useState([]);
    const [status, setStatus] = useState("");

    useEffect(() => {
        const loadTransportModes = async () => {
            try {
                const response = await fetch("http://localhost:5292/api/transportmodes", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    credentials: "include"
                });

                if (!response.ok) {
                    throw new Error("Błąd HTTP " + response.status);
                }

                const data = await response.json();
                setAllTransportModes(data || []);
            } catch (e) {
                console.error("Błąd ładowania środków transportu:", e);
                setStatus("Nie udało się pobrać listy środków transportu.");
            }
        };

        loadTransportModes();
    }, []);

    useEffect(() => {
        if (!user || allTransportModes.length === 0) return;

        const ids = getUserTransportModeIds(user, allTransportModes);
        setSelectedTransportModes(ids);
    }, [user, allTransportModes]);

    function handleToggleTransportMode(id) {
        setSelectedTransportModes((prev) => {
            if (prev.includes(id)) {
                return prev.filter((modeId) => modeId !== id);
            }

            return [...prev, id];
        });
    }

    async function refreshUser() {
        const meRes = await fetch("http://localhost:5292/api/users/me", {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            },
            credentials: "include"
        });

        if (!meRes.ok) {
            throw new Error("Nie udało się pobrać aktualnych danych użytkownika.");
        }

        const freshUser = await meRes.json();
        setUser(freshUser);
        return freshUser;
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/me", {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include",
                body: JSON.stringify({
                    transportModeIds: selectedTransportModes
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zapisać środków transportu.");
                return;
            }

            await refreshUser();
            navigate(-1);
        } catch (e) {
            console.error("Błąd zapisu środków transportu:", e);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    const handleRemoveAll = async () => {
        setStatus("");

        if (selectedTransportModes.length === 0) {
            setStatus("Nie masz ustawionych środków transportu.");
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/me", {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include",
                body: JSON.stringify({
                    transportModeIds: []
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się usunąć środków transportu.");
                return;
            }

            await refreshUser();
            navigate(-1);
        } catch (e) {
            console.error("Błąd usuwania środków transportu:", e);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    return (
        <main className="settings-page">
            <section className="settings-panel settings-panel--form" aria-label="Ustawienia">
                <header className="settings-header">
                    <h1 className="settings-title">Ustawienia</h1>
                    <p className="settings-subtitle">Edytuj preferowane środki transportu</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">
                                Wybierz preferowane środki transportu
                            </label>

                            <div className="pill-group">
                                {allTransportModes.length > 0 ? (
                                    allTransportModes.map((mode) => {
                                        const isSelected = selectedTransportModes.includes(mode.id);

                                        return (
                                            <button
                                                key={mode.id}
                                                type="button"
                                                className={`pill pill--selectable ${isSelected ? "pill--selected" : ""}`}
                                                onClick={() => handleToggleTransportMode(mode.id)}
                                                aria-pressed={isSelected}
                                            >
                                                <span>{mode.name}</span>
                                                {isSelected && (
                                                    <span className="pill__close" aria-hidden="true">
                                                        ×
                                                    </span>
                                                )}
                                            </button>
                                        );
                                    })
                                ) : (
                                    <p className="settings-status">
                                        Brak dostępnych środków transportu.
                                    </p>
                                )}
                            </div>
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
                            onClick={handleRemoveAll}
                            disabled={loading}
                        >
                            Usuń wszystkie
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